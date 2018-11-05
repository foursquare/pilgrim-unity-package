//
//  PilgrimClientInterface.m
//  PilgrimUnitySDK
//
//  Copyright © 2018 Foursquare. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <Pilgrim/Pilgrim.h>

void PilgrimStart() {
    [[FSQPPilgrimManager sharedManager] start];
}

void PilgrimStop() {
    [[FSQPPilgrimManager sharedManager] stop];
}

void PilgrimClearAllData() {
    [[FSQPPilgrimManager sharedManager] clearAllData:nil];
}
