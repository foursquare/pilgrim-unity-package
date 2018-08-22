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

void ReadLogs()
{
    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
    NSArray<FSQPDebugLog *> *logs = [logger performSelector:NSSelectorFromString(@"allLogs")];
    for (FSQPDebugLog *log in logs) {
//        NSLog(@"LOG: %@", log);
    }
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(3.0 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        ReadLogs();
    });
}

void Start(const char * consumerKey, const char * consumerSecret)
{
    NSString *consumerKeyNSString = [NSString stringWithCString:consumerKey encoding:NSUTF8StringEncoding];
    NSString *consumerSecretNSString = [NSString stringWithCString:consumerSecret encoding:NSUTF8StringEncoding];
    delegate = [[PilgrimDelegate alloc] init];
 
    Method original = class_getInstanceMethod(NSClassFromString(@"FSQPLogger"), NSSelectorFromString(@"logLevel:type:event:data:isPublic:"));
    Method swizzled = class_getInstanceMethod([NSObject class], @selector(xxx_logLevel:type:event:data:isPublic:));
    method_exchangeImplementations(original, swizzled);
    
//    id logger = [NSClassFromString(@"FSQPLogger") performSelector:NSSelectorFromString(@"sharedLogger")];
//    [logger performSelector:NSSelectorFromString(@"setAllowsNonPublicLogs:") withObject:@YES];
//    [logger performSelector:NSSelectorFromString(@"setEnabled:") withObject:@YES];
    
    [FSQPPilgrimManager sharedManager].debugLoggingEnabled = YES;
    
    [[FSQPPilgrimManager sharedManager] configureWithConsumerKey:consumerKeyNSString
                                                          secret:consumerSecretNSString
                                                        delegate:(PilgrimDelegate * _Nonnull)delegate
                                                      completion:^(BOOL didSucceed, NSError * _Nullable error) {
                                                          [[FSQPPilgrimManager sharedManager] startMonitoringVisits];
                                                      }];
    ReadLogs();
}

void Stop()
{
    [[FSQPPilgrimManager sharedManager] stopMonitoringVisits];
}

void SetOauthToken(const char * oauthToken)
{
    [FSQPPilgrimManager sharedManager].oauthToken = [NSString stringWithCString:oauthToken encoding:NSUTF8StringEncoding];
}
