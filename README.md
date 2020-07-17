## TalkingData Game Analytics Unity SDK
Game Analytics Unity 平台 SDK 由 `封装层` 和 `Native SDK` 两部分构成，目前 GitHub上 提供了封装层代码，需要从 [TalkingData官网](https://www.talkingdata.com/spa/sdk/#/config) 下载最新版的 Android 和 iOS 平台 Native SDK，组合使用。

### 集成说明
1. 下载本项目（封装层）到本地；  
2. 访问 [TalkingData官网](https://www.talkingdata.com/spa/sdk/#/config) 下载最新版的 Android 和 iOS 平台 Game Analytics SDK（ Native SDK）
	- 方法1：选择 Unity 平台进行功能定制；
	- 方法2：分别选择 Android 和 iOS 平台进行功能定制，请确保两个平台功能项一致；  
3. 将下载的最新版 `Native SDK` 复制到 `封装层` 中，构成完整的 Unity SDK。  
	- Android 平台  
	将最新的 `.jar` 文件复制到 `Assets/Plugins/Android` 目录下
	- iOS 平台  
	将最新的 `.h` 和 `.a` 文件复制到 `Assets/Plugins/iOS` 目录下
4. 按 `Native SDK` 功能选项对 `封装层` 代码进行必要的修改，详见“注意事项”第2条；
5. 将 Unity SDK 集成您需要统计的工程中，并按 [集成文档](http://doc.talkingdata.com/posts/65) 进行必要配置和功能调用。

### 注意事项
1. 分别选择 Android 和 iOS 平台进行功能定制时，请确保两个平台功能项一致。
2. 如果申请 Native SDK 时选择了可选功能，则需要在本项目中启用所选功能对应的封装层代码。  
	a) 在 `Assets/Plugins/iOS/TalkingDataGA.mm` 文件中释放所选功能的宏定义。  
	b) 在 Unity 中添加相应功能的宏定义  
	打开 `Build Settings`，先在 `Platform` 中选择 `Android` 或 `iOS` 平台，再点击 `Switch Platform`。当切换完平台后，点击 `Player Settings`，然后在 `Other Settings` 的 `Scripting Define Symbols` 中输入所选功能相应的宏（如果有多个宏，需要用分号隔开；Android 和 iOS 需要分别添加）。
	
	各功能宏定义如下:
	
	| 功能       | 宏定义      |
	| ---------- | ----------- |
	| 自定义事件 | TDGA_CUSTOM |
	| 推送营销   | TDGA_PUSH   |
