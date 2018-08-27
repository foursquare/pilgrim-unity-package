#import "PilgrimUnitySDK.h"

@interface PilgrimUnitySDK ()

// NOTE(rojas) Private for now, will probably remove logs functionality or keep private
- (NSString *)getLogsJSON;

@end

void RequestLocationPermission()
{
    [[PilgrimUnitySDK shared] requestLocationPermission:^(BOOL didAuthorize) {
        UnitySendMessage("_PilgrimCallbacks", "OnPermissionsGranted", didAuthorize ? "true" : "false");
    }];
}

void Start(const char * consumerKey, const char * consumerSecret)
{
    NSString *consumerKeyNSString = [NSString stringWithCString:consumerKey encoding:NSUTF8StringEncoding];
    NSString *consumerSecretNSString = [NSString stringWithCString:consumerSecret encoding:NSUTF8StringEncoding];
    [[PilgrimUnitySDK shared] startWithConsumerKey:consumerKeyNSString secret:consumerSecretNSString];
}

void Stop()
{
    [[PilgrimUnitySDK shared] stop];
}

void SetOauthToken(const char * oauthToken)
{
    [PilgrimUnitySDK shared].oauthToken = [NSString stringWithCString:oauthToken encoding:NSUTF8StringEncoding];
}

const char * DoGetLogs()
{
    NSString *logsJSON = [[PilgrimUnitySDK shared] getLogsJSON];
    
    // Need to allocate on heap unity will deallocate
    const char * logsJSONC = malloc(logsJSON.length + 1);
    memset((void *)logsJSONC, 0, logsJSON.length + 1);
    memcpy((void *)logsJSONC, [logsJSON cStringUsingEncoding:NSUTF8StringEncoding], logsJSON.length);

    return logsJSONC;
}
