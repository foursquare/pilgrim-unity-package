#import "PilgrimUnitySDK.h"

#import <objc/runtime.h>
#import <Pilgrim/Pilgrim.h>

@interface NSObject (Swizzled)
@end

@implementation NSObject (Swizzled)

- (void)xxx_logLevel:(FSQPLogLevel)level
                type:(FSQPLogType)type
               event:(NSString *)event
                data:(nullable NSObject<NSCoding> *)data
            isPublic:(BOOL)isPublic
{
    [self xxx_logLevel:level type:type event:event data:data isPublic:YES];
}

@end

@interface NSMutableArray (Reverse)
@end

@implementation NSMutableArray (Reverse)

- (void)reverse
{
    if ([self count] <= 1)
        return;
    NSUInteger i = 0;
    NSUInteger j = [self count] - 1;
    while (i < j) {
        [self exchangeObjectAtIndex:i
                  withObjectAtIndex:j];
        
        i++;
        j--;
    }
}

@end

@interface PilgrimUnityDelegate : NSObject <FSQPPilgrimManagerDelegate>
@end

@interface PilgrimUnitySDK ()

@property (nonatomic) PilgrimUnityDelegate *delegate;

@property (nonatomic, getter=isRunning) BOOL running;

@end

@implementation PilgrimUnitySDK

+ (instancetype)shared
{
    static id instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[self alloc] init];
    });
    return instance;
}

- (instancetype)init
{
    self = [super init];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(didBecomeActive) name:UIApplicationDidBecomeActiveNotification object:nil];
    return self;
}

- (void)setRunning:(BOOL)running
{
    [[NSUserDefaults standardUserDefaults] setObject:@(running) forKey:@"PilgrimUnitySDKIsRunning"];
}

- (BOOL)isRunning
{
    return [[NSUserDefaults standardUserDefaults] boolForKey:@"PilgrimUnitySDKIsRunning"];
}

- (void)requestLocationPermission:(void (^)(BOOL))completion
{
    [[FSQPPilgrimManager sharedManager] requestAlwaysAuthorizationWithCompletion:^(BOOL didAuthorize) {
        completion(didAuthorize);
    }];
}

- (void)startWithConsumerKey:(NSString *)key secret:(NSString *)secret
{
    self.delegate = [[PilgrimUnityDelegate alloc] init];
    
    // DOESN'T WORK SO SWIZZLE TO GET LOGS TO BE PUBLIC
//    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
//    [logger performSelector:NSSelectorFromString(@"setAllowsNonPublicLogs:") withObject:@YES];
//    [logger performSelector:NSSelectorFromString(@"setEnabled:") withObject:@YES];
    
    Method original = class_getInstanceMethod(NSClassFromString(@"FSQPLogger"), NSSelectorFromString(@"logLevel:type:event:data:isPublic:"));
    Method swizzled = class_getInstanceMethod([NSObject class], @selector(xxx_logLevel:type:event:data:isPublic:));
    method_exchangeImplementations(original, swizzled);
    
    [FSQPPilgrimManager sharedManager].debugLoggingEnabled = YES;
    
    void (^configureCompletion)(BOOL, NSError *) = ^(BOOL didSucceed, NSError * _Nullable error) {
        [[FSQPPilgrimManager sharedManager] startMonitoringVisits];
        self.running = YES;
    };
    
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:key
                                                          secret:secret
                                                        delegate:self.delegate
                                                      completion:configureCompletion];
}

- (void)stop
{
    [[FSQPPilgrimManager sharedManager] stopMonitoringVisits];
    self.running = NO;
}

- (void)setOauthToken:(NSString *)oauthToken
{
    [FSQPPilgrimManager sharedManager].oauthToken = oauthToken;
}

- (NSString *)cleanLog:(NSString *)input
{
    if (!input) {
        return @"";
    }
    NSMutableCharacterSet *keep = [NSMutableCharacterSet alphanumericCharacterSet];
    [keep formUnionWithCharacterSet:[NSCharacterSet characterSetWithCharactersInString:@"<>.,:{}[]-+() /"]];
    NSCharacterSet *remove = [keep invertedSet];
    return [[input componentsSeparatedByCharactersInSet:remove] componentsJoinedByString:@""];
}

- (NSString *)getLogsJSON
{
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
    NSArray<FSQPDebugLog *> *logsa = [logger performSelector:NSSelectorFromString(@"allLogs")];
#pragma clang diagnostic pop
    NSMutableArray *logs = [logsa mutableCopy];
    [logs reverse];
    
    NSMutableString *logsJSON = [NSMutableString stringWithString:@"{\"Items\":["];
    
    for (FSQPDebugLog *log in logs) {
        NSMutableString *logJSON = [NSMutableString stringWithString:@"{"];
        [logJSON appendFormat:@"\"title\":\"%@\",", [self cleanLog:log.eventDescription]];
        [logJSON appendFormat:@"\"description\":\"%@\",", [self cleanLog:[log.data description]]];
        [logJSON appendFormat:@"\"timestamp\":\"%f\"", log.timestamp.timeIntervalSince1970];
        [logJSON appendString:@"},"];
        [logsJSON appendString:logJSON];
    }
    
    
    [logsJSON deleteCharactersInRange:NSMakeRange(logsJSON.length - 1, 1)];
    [logsJSON appendString:@"]}"];
    return logsJSON;
}

- (void)didBecomeActive
{
    NSArray<FSQPGeofenceEvent *> *cachedGeofenceEvents = [NSKeyedUnarchiver unarchiveObjectWithFile:[[self class] geofenceEventsArchivePath]] ?: @[];
    if ([cachedGeofenceEvents count] > 0) {
        [self deliverGeofenceEvents:cachedGeofenceEvents];
        [[NSFileManager defaultManager] removeItemAtPath:[[self class] geofenceEventsArchivePath] error:nil];
    }
}

- (void)deliverGeofenceEvents:(NSArray< FSQPGeofenceEvent *> *)geofenceEvents
{
    NSMutableString *geofenceEventsJSON = [NSMutableString stringWithString:@"{\"Items\":["];
    
    for (FSQPGeofenceEvent *geofenceEvent in geofenceEvents) {
        NSString *eventType;
        switch (geofenceEvent.eventType) {
            case FSQPGeofenceEventTypeEntrance:
                eventType = @"entrance";
                break;
            case FSQPGeofenceEventTypeDwell:
                eventType = @"dwell";
                break;
            case FSQPGeofenceEventTypeExit:
                eventType = @"exit";
                break;
        }
        
        NSMutableString *geofenceEventJSON = [NSMutableString stringWithString:@"{"];
        [geofenceEventJSON appendFormat:@"\"eventType\":\"%@\",", eventType];
        [geofenceEventJSON appendFormat:@"\"venueID\":\"%@\",", geofenceEvent.venueID];
        [geofenceEventJSON appendString:@"\"categoryIDs\":[],"];
        [geofenceEventJSON appendString:@"\"chainIDs\":[],"];
        [geofenceEventJSON appendString:@"\"partnerVenueID\":\"\","];
        [geofenceEventJSON appendFormat:@"\"venue\":{\"name\":\"%@\"},", geofenceEvent.venue.name];
        [geofenceEventJSON appendFormat:@"\"location\":{\"lat\":%f,\"lng\":%f,\"hacc\":%f},", geofenceEvent.location.coordinate.latitude, geofenceEvent.location.coordinate.longitude, geofenceEvent.location.horizontalAccuracy];
        [geofenceEventJSON appendFormat:@"\"timestamp\":%f", geofenceEvent.timestamp.timeIntervalSince1970];
        [geofenceEventJSON appendString:@"},"];
        [geofenceEventsJSON appendString:geofenceEventJSON];
    }
    
    [geofenceEventsJSON deleteCharactersInRange:NSMakeRange(geofenceEventsJSON.length - 1, 1)];
    [geofenceEventsJSON appendString:@"]}"];
    
    UnitySendMessage("_PilgrimCallbacks", "OnGeofenceEvents", [geofenceEventsJSON cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)saveGeofenceEvents:(NSArray<FSQPGeofenceEvent *> *)geofenceEvents
{
    NSMutableArray<FSQPGeofenceEvent *> *cachedGeofenceEvents = [[NSKeyedUnarchiver unarchiveObjectWithFile:[[self class] geofenceEventsArchivePath]] mutableCopy] ?: [@[] mutableCopy];
    [cachedGeofenceEvents addObjectsFromArray:geofenceEvents];
    [NSKeyedArchiver archiveRootObject:cachedGeofenceEvents toFile:[[self class] geofenceEventsArchivePath]];
}

+ (NSString *)geofenceEventsArchivePath
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    return [documentsDirectory stringByAppendingPathComponent:@"PilgrimGeofenceEvents.archive"];
}

@end

@implementation PilgrimUnityDelegate

- (void)fsqpPilgrimManager:(FSQPPilgrimManager *)pilgrimManager didVisit:(FSQPVisit *)visit
{
    
}

- (void)fsqpPilgrimManager:(FSQPPilgrimManager *)pilgrimManager didBackfillVisit:(FSQPVisit *)visit
{
    
}

- (void)fsqpPilgrimManager:(FSQPPilgrimManager *)pilgrimManager didDetectNearbyVenues:(NSArray<FSQPNearbyVenue *> *)nearbyVenues
{
    
}

- (void)fsqpPilgrimManager:(FSQPPilgrimManager *)pilgrimManager didReceiveGeofenceNotification:(FSQPGeofenceNotification *)geofenceNotification
{
    if ([UIApplication sharedApplication].applicationState == UIApplicationStateActive) {
        [[PilgrimUnitySDK shared] deliverGeofenceEvents:geofenceNotification.events];
    } else {
        [[PilgrimUnitySDK shared] saveGeofenceEvents:geofenceNotification.events];
    }
}

@end
