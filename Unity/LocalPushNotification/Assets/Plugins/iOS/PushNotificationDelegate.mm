#import "PushNotificationDelegate.h"

 extern "C" {
bool endGetToken = false;
char* deviceToken;

	void popupDisp(char* charData) {
        
        deviceToken = (char*)malloc(strlen(charData) + 1);
        strcpy(deviceToken, charData);
        endGetToken = true;
	}
	bool GotDeviceToken()
	{
		return endGetToken;
	}
	char* GetDeviceTokenChr()
	{
		if (deviceToken == NULL)
			return NULL;
		
		
		return deviceToken;
	}
	void StartDeviceToken()
	{
		if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 8.0)
		{
			[[UIApplication sharedApplication] registerUserNotificationSettings:[UIUserNotificationSettings settingsForTypes:(UIUserNotificationTypeSound | UIUserNotificationTypeAlert | UIUserNotificationTypeBadge) categories:nil]];
			[[UIApplication sharedApplication] registerForRemoteNotifications];
	    }
	    else
	    {
	        [[UIApplication sharedApplication] registerForRemoteNotificationTypes:
	         (UIUserNotificationTypeBadge | UIUserNotificationTypeSound | UIUserNotificationTypeAlert)];
	    }
	}
}

@implementation PushNotificationDelegate
- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)devToken {

    [super application:application didRegisterForRemoteNotificationsWithDeviceToken:devToken];
    
     UIAlertView *alertView = [[UIAlertView alloc]
                              initWithTitle:@"deviceToken"
                              message:devToken.description
                              delegate:self
                              cancelButtonTitle:@"Cancel"
                              otherButtonTitles:@"OK", nil];
    [alertView show];
    
	char* token = (char*)[devToken.description UTF8String];
	popupDisp(token);
}
 
@end