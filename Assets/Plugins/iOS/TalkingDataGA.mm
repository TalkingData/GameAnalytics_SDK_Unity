#import "TalkingDataGA.h"

//#define TDGA_CUSTOM     // 自定义事件
//#define TDGA_PUSH       // 推送营销

// Converts C style string to NSString
static NSString *TDGACreateNSString(const char *string) {
    return string ? [NSString stringWithUTF8String:string] : nil;
}

static char *tdgaDeviceId = NULL;
static TDGAProfile *tdgaProfile = nil;

extern "C" {
#pragma GCC diagnostic ignored "-Wmissing-prototypes"

const char *TDGAGetDeviceId() {
    if (!tdgaDeviceId) {
        NSString *deviceId = [TalkingDataGA getDeviceId];
        tdgaDeviceId = (char *)calloc(deviceId.length + 1, sizeof(char));
        strcpy(tdgaDeviceId, deviceId.UTF8String);
    }
    return tdgaDeviceId;
}

void TDGASetVerboseLogDisabled() {
    [TalkingDataGA setVerboseLogDisabled];
}

void TDGABackgroundSessionEnabled() {
    [TalkingDataGA backgroundSessionEnabled];
}

void TDGAOnStart(const char *appId, const char *channelId) {
    if ([TalkingDataGA respondsToSelector:@selector(setFrameworkTag:)]) {
        [TalkingDataGA performSelector:@selector(setFrameworkTag:) withObject:@2];
    }
    [TalkingDataGA onStart:TDGACreateNSString(appId) withChannelId:TDGACreateNSString(channelId)];
}

void TDGASetLocation(double latitude, double longitude) {
    [TalkingDataGA setLatitude:latitude longitude:longitude];
}

void TDGASetProfile(const char *profileId) {
    tdgaProfile = [TDGAProfile setProfile:TDGACreateNSString(profileId)];
}

void TDGASetProfileName(const char *profileName) {
    if (nil != tdgaProfile) {
        [tdgaProfile setProfileName:TDGACreateNSString(profileName)];
    }
}

void TDGASetProfileType(int profileType) {
    if (nil != tdgaProfile) {
        [tdgaProfile setProfileType:(TDGAProfileType)profileType];
    }
}

void TDGASetLevel(int level) {
    if (nil != tdgaProfile) {
        [tdgaProfile setLevel:level];
    }
}

void TDGASetGender(int gender) {
    if (nil != tdgaProfile) {
        [tdgaProfile setGender:(TDGAGender)gender];
    }
}

void TDGASetAge(int age) {
    if (nil != tdgaProfile) {
        [tdgaProfile setAge:age];
    }
}

void TDGASetGameServer(const char *gameServer) {
    if (nil != tdgaProfile) {
        [tdgaProfile setGameServer:TDGACreateNSString(gameServer)];
    }
}

void TDGAOnBegin(const char *missionId) {
    [TDGAMission onBegin:TDGACreateNSString(missionId)];
}

void TDGAOnCompleted(const char *missionId) {
    [TDGAMission onCompleted:TDGACreateNSString(missionId)];
}

void TDGAOnFailed(const char *missionId, const char *failedCause) {
    [TDGAMission onFailed:TDGACreateNSString(missionId) failedCause:TDGACreateNSString(failedCause)];
}

void TDGAOnChargeRequst(const char *orderId, const char *iapId, double currencyAmount, const char *currencyType, double virtualCurrencyAmount, const char *paymentType) {
    [TDGAVirtualCurrency onChargeRequst:TDGACreateNSString(orderId)
                                  iapId:TDGACreateNSString(iapId)
                         currencyAmount:currencyAmount
                           currencyType:TDGACreateNSString(currencyType)
                  virtualCurrencyAmount:virtualCurrencyAmount
                            paymentType:TDGACreateNSString(paymentType)];
}

void TDGAOnChargSuccess(const char *orderId) {
    [TDGAVirtualCurrency onChargeSuccess:TDGACreateNSString(orderId)];
}

void TDGAOnReward(double virtualCurrencyAmount, const char *reason) {
    [TDGAVirtualCurrency onReward:virtualCurrencyAmount reason:TDGACreateNSString(reason)];
}

void TDGAOnPurchase(const char *item, int itemNumber, double priceInVirtualCurrency) {
    [TDGAItem onPurchase:TDGACreateNSString(item) itemNumber:itemNumber priceInVirtualCurrency:priceInVirtualCurrency];
}

void TDGAOnUse(const char *item, int itemNumber) {
    [TDGAItem onUse:TDGACreateNSString(item) itemNumber:itemNumber];
}

#ifdef TDGA_CUSTOM
void TDGAOnEvent(const char *eventId, const char *parameters) {
    NSString *parameterStr = TDGACreateNSString(parameters);
    NSDictionary *parameterDic = nil;
    if (parameterStr) {
        NSData *parameterData = [parameterStr dataUsingEncoding:NSUTF8StringEncoding];
        parameterDic = [NSJSONSerialization JSONObjectWithData:parameterData options:0 error:nil];
    }
    [TalkingDataGA onEvent:TDGACreateNSString(eventId) eventData:parameterDic];
}
#endif

#ifdef TDGA_PUSH
void TDGASetDeviceToken(const void *deviceToken, int length) {
    NSData *tokenData = [NSData dataWithBytes:deviceToken length:length];
    [TalkingDataGA setDeviceToken:tokenData];
}

void TDGAHandlePushMessage(const char *message) {
    NSString *val = TDGACreateNSString(message);
    NSDictionary *dic = [NSDictionary dictionaryWithObject:val forKey:@"sign"];
    [TalkingDataGA handleTDGAPushMessage:dic];
}
#endif

#pragma GCC diagnostic warning "-Wmissing-prototypes"
}
