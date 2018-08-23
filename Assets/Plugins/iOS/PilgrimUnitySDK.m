#import <Pilgrim/Pilgrim.h>
#import <objc/runtime.h>

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

@interface PilgrimDelegate : NSObject <FSQPPilgrimManagerDelegate>

@end

@implementation PilgrimDelegate

- (void)fsqpPilgrimManager:(nonnull FSQPPilgrimManager *)pilgrimManager didVisit:(nonnull FSQPVisit *)visit
{
    
}

- (void)fsqpPilgrimManager:(nonnull FSQPPilgrimManager *)pilgrimManager didBackfillVisit:(nonnull FSQPVisit *)visit
{
    
}

- (void)fsqpPilgrimManager:(FSQPPilgrimManager *)pilgrimManager didDetectNearbyVenues:(NSArray<FSQPNearbyVenue *> *)nearbyVenues
{
    
}

- (void)fsqpPilgrimManager:(FSQPPilgrimManager *)pilgrimManager didReceiveGeofenceNotification:(FSQPGeofenceNotification *)geofenceNotification
{
    NSMutableString *geofenceEventsJSON = [NSMutableString stringWithString:@"{\"Items\":["];
    
    for (FSQPGeofenceEvent *geofenceEvent in geofenceNotification.events) {
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
        [geofenceEventJSON appendString:@"\"categoryIDs\":[\"a\",\"b\",\"c\"],"];
        [geofenceEventJSON appendString:@"\"chainIDs\":[\"1\",\"2\",\"3\"],"];
        [geofenceEventJSON appendString:@"\"partnerVenueID\":\"partnerVenueID\""];
        [geofenceEventJSON appendString:@"},"];
        [geofenceEventsJSON appendString:geofenceEventJSON];
    }
    
    [geofenceEventsJSON deleteCharactersInRange:NSMakeRange(geofenceEventsJSON.length - 1, 1)];
    [geofenceEventsJSON appendString:@"]}"];
    UnitySendMessage("_PilgrimCallbacks", "OnGeofenceEvents", [geofenceEventsJSON cStringUsingEncoding:NSUTF8StringEncoding]);
}

@end

static PilgrimDelegate * delegate = NULL;

void RequestPermissions()
{
    [[FSQPPilgrimManager sharedManager] requestAlwaysAuthorizationWithCompletion:^(BOOL didAuthorize) {
        UnitySendMessage("_PilgrimCallbacks", "OnPermissionsGranted", didAuthorize ? "true" : "false");
    }];
}

void Start(const char * consumerKey, const char * consumerSecret)
{
    NSString *consumerKeyNSString = [NSString stringWithCString:consumerKey encoding:NSUTF8StringEncoding];
    NSString *consumerSecretNSString = [NSString stringWithCString:consumerSecret encoding:NSUTF8StringEncoding];
    delegate = [[PilgrimDelegate alloc] init];
    
    // DOESN'T WORK SO SWIZZLE TO GET LOGS TO BE PUBLIC
//    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
//    [logger performSelector:NSSelectorFromString(@"setAllowsNonPublicLogs:") withObject:@YES];
//    [logger performSelector:NSSelectorFromString(@"setEnabled:") withObject:@YES];
 
    Method original = class_getInstanceMethod(NSClassFromString(@"FSQPLogger"), NSSelectorFromString(@"logLevel:type:event:data:isPublic:"));
    Method swizzled = class_getInstanceMethod([NSObject class], @selector(xxx_logLevel:type:event:data:isPublic:));
    method_exchangeImplementations(original, swizzled);
    
    [FSQPPilgrimManager sharedManager].debugLoggingEnabled = YES;
    
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:consumerKeyNSString
                                                          secret:consumerSecretNSString
                                                        delegate:(PilgrimDelegate * _Nonnull)delegate
                                                      completion:^(BOOL didSucceed, NSError * _Nullable error) {
                                                          [[FSQPPilgrimManager sharedManager] startMonitoringVisits];
                                                      }];
}

void Stop()
{
    [[FSQPPilgrimManager sharedManager] stopMonitoringVisits];
}

void SetOauthToken(const char * oauthToken)
{
    [FSQPPilgrimManager sharedManager].oauthToken = [NSString stringWithCString:oauthToken encoding:NSUTF8StringEncoding];
}

@interface NSMutableArray (Reverse)

@end

@implementation NSMutableArray (Reverse)

- (void)reverse {
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

static NSString * _Clean(NSString *input)
{
    if (!input) {
        return @"";
    }
    NSMutableCharacterSet *keep = [NSMutableCharacterSet alphanumericCharacterSet];
    [keep formUnionWithCharacterSet:[NSCharacterSet characterSetWithCharactersInString:@"<>.,:{}[]-+() "]];
    NSCharacterSet *remove = [keep invertedSet];
    return [[input componentsSeparatedByCharactersInSet:remove] componentsJoinedByString:@""];
}

const char * _GetLogs()
{
    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
    NSArray<FSQPDebugLog *> *logsa = [logger performSelector:NSSelectorFromString(@"allLogs")];
    NSMutableArray *logs = [logsa mutableCopy];
    [logs reverse];
    
    NSMutableString *logsJSON = [NSMutableString stringWithString:@"{\"Items\":["];
    
    for (FSQPDebugLog *log in logs) {
        NSMutableString *logJSON = [NSMutableString stringWithString:@"{"];
        [logJSON appendFormat:@"\"title\":\"%@\",", _Clean(log.eventDescription)];
        [logJSON appendFormat:@"\"description\":\"%@\"", _Clean([log.data description])];
        [logJSON appendString:@"},"];
        [logsJSON appendString:logJSON];
    }
    
    
    [logsJSON deleteCharactersInRange:NSMakeRange(logsJSON.length - 1, 1)];
    [logsJSON appendString:@"]}"];
    
    const char * logsJSONC = malloc(logsJSON.length + 1);
    memset((void *)logsJSONC, 0, logsJSON.length + 1);
    memcpy((void *)logsJSONC, [logsJSON cStringUsingEncoding:NSUTF8StringEncoding], logsJSON.length);
    
    return logsJSONC;
}
