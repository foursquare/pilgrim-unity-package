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
    
    [FSQPPilgrimManager sharedManager].debugLogsEnabled = YES;
    
    void (^configureCompletion)(BOOL, NSError *) = ^(BOOL didSucceed, NSError * _Nullable error) {
        [[FSQPPilgrimManager sharedManager] start];
        self.running = YES;
    };
    
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:key
                                                          secret:secret
                                                        delegate:self.delegate
                                                      completion:configureCompletion];
}

- (void)stop
{
    [[FSQPPilgrimManager sharedManager] stop];
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
    input = [[input componentsSeparatedByCharactersInSet:remove] componentsJoinedByString:@""];
    NSMutableString *s = [NSMutableString stringWithString:input];
    [s replaceOccurrencesOfString:@"\"" withString:@"\\\"" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    [s replaceOccurrencesOfString:@"/" withString:@"\\/" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    [s replaceOccurrencesOfString:@"\n" withString:@"\\n" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    [s replaceOccurrencesOfString:@"\b" withString:@"\\b" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    [s replaceOccurrencesOfString:@"\f" withString:@"\\f" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    [s replaceOccurrencesOfString:@"\r" withString:@"\\r" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    [s replaceOccurrencesOfString:@"\t" withString:@"\\t" options:NSCaseInsensitiveSearch range:NSMakeRange(0, [s length])];
    return [NSString stringWithString:s];
}

- (NSString *)getLogsJSON
{
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
    NSArray<FSQPDebugLog *> *logsInOrder = [logger performSelector:NSSelectorFromString(@"allLogs")];
#pragma clang diagnostic pop
    NSMutableArray *logs = [logsInOrder mutableCopy];
    [logs reverse];
    
    NSMutableString *logsJSON = [NSMutableString stringWithString:@"{\"Items\":["];
    
    for (FSQPDebugLog *log in logs) {
        NSMutableString *logJSON = [NSMutableString stringWithString:@"{"];
        [logJSON appendFormat:@"\"title\":\"%@\",", [self cleanLog:log.eventDescription]];
        NSString *description = [self cleanLog:[log.data description]];
        [logJSON appendFormat:@"\"description\":\"%@\",", description];
        [logJSON appendFormat:@"\"timestamp\":%f", log.timestamp.timeIntervalSince1970];
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
    NSMutableArray<FSQPVisit *> *cachedVisits = [NSKeyedUnarchiver unarchiveObjectWithFile:[[self class] visitsArchivePath]] ?: @[];
    if ([cachedVisits count] > 0) {
        for (FSQPVisit *visit in cachedVisits) {
            [self deliverVisit:visit];
        }
        [[NSFileManager defaultManager] removeItemAtPath:[[self class] visitsArchivePath] error:nil];
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
        
        FSQPCategory *primaryCategory = nil;
        for (FSQPCategory *category in geofenceEvent.venue.categories) {
            if (primaryCategory == nil || category.isPrimary) {
                primaryCategory = category;
            }
        }
        
        [geofenceEventJSON appendFormat:@"\"venue\":{\"name\":\"%@\",\"category\":{\"name\":\"%@\",\"icon\":\"%@88%@\"}},", geofenceEvent.venue.name, primaryCategory.name, primaryCategory.icon.prefix, primaryCategory.icon.suffix];
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

- (void)deliverVisit:(FSQPVisit *)visit
{
    NSMutableString *visitJSON = [NSMutableString stringWithString:@"{"];
    
    [visitJSON appendFormat:@"\"isArrival\":%@,", visit.isArrival ? @"true" : @"false"];
    
    FSQPCategory *primaryCategory = nil;
    for (FSQPCategory *category in visit.venue.categories) {
        if (primaryCategory == nil || category.isPrimary) {
            primaryCategory = category;
        }
    }
    
    [visitJSON appendFormat:@"\"venue\":{\"name\":\"%@\",\"category\":{\"name\":\"%@\",\"icon\":\"%@88%@\"}},", visit.venue.name, primaryCategory.name, primaryCategory.icon.prefix, primaryCategory.icon.suffix];
    
    if (visit.isArrival) {
        [visitJSON appendFormat:@"\"timestamp\":%f", visit.arrivalDate.timeIntervalSince1970];
    } else {
        [visitJSON appendFormat:@"\"timestamp\":%f", visit.departureDate.timeIntervalSince1970];
    }
    
    [visitJSON appendString:@"}"];
    
    UnitySendMessage("_PilgrimCallbacks", "OnVisit", [visitJSON cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)saveVisit:(FSQPVisit *)visit
{
    NSMutableArray<FSQPVisit *> *cachedVisits = [[NSKeyedUnarchiver unarchiveObjectWithFile:[[self class] visitsArchivePath]] mutableCopy] ?: [@[] mutableCopy];
    [cachedVisits addObject:visit];
    [NSKeyedArchiver archiveRootObject:cachedVisits toFile:[[self class] visitsArchivePath]];
}

+ (NSString *)geofenceEventsArchivePath
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    return [documentsDirectory stringByAppendingPathComponent:@"PilgrimGeofenceEvents.archive"];
}

+ (NSString *)visitsArchivePath
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    return [documentsDirectory stringByAppendingPathComponent:@"PilgrimVisits.archive"];
}

@end

@implementation PilgrimUnityDelegate

- (void)pilgrimManager:(nonnull FSQPPilgrimManager *)pilgrimManager handleVisit:(nonnull FSQPVisit *)visit
{
    if ([UIApplication sharedApplication].applicationState == UIApplicationStateActive) {
        [[PilgrimUnitySDK shared] deliverVisit:visit];
    } else {
        [[PilgrimUnitySDK shared] saveVisit:visit];
    }
}

- (void)pilgrimManager:(FSQPPilgrimManager *)pilgrimManager handleGeofenceEvents:(NSArray<FSQPGeofenceEvent *> *)geofenceEvents
{
    if ([UIApplication sharedApplication].applicationState == UIApplicationStateActive) {
        [[PilgrimUnitySDK shared] deliverGeofenceEvents:geofenceEvents];
    } else {
        [[PilgrimUnitySDK shared] saveGeofenceEvents:geofenceEvents];
    }
}

@end
