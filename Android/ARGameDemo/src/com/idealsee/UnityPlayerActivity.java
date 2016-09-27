package com.idealsee;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.Window;

import com.baidu.mapapi.SDKInitializer;
import com.unity3d.player.UnityPlayer;

public class UnityPlayerActivity extends Activity {
	protected UnityPlayer mUnityPlayer; // don't change the name of this
										// variable; referenced from native code

	// Setup activity layout
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia
														// play happy

		mUnityPlayer = new UnityPlayer(this);
		setContentView(mUnityPlayer);
		mUnityPlayer.requestFocus();

		initVerifyKey();
	}

	public void loadBaseMap() {
		Intent intent = new Intent(UnityPlayerActivity.this, BaseMapDemo.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
		this.startActivity(intent);
	}

	// Quit Unity
	@Override
	protected void onDestroy() {
		mUnityPlayer.quit();
		super.onDestroy();
		unregisterReceiver(mReceiver);
	}

	// Pause Unity
	@Override
	protected void onPause() {
		super.onPause();
		mUnityPlayer.pause();
	}

	// Resume Unity
	@Override
	protected void onResume() {
		super.onResume();
		mUnityPlayer.resume();
	}

	// This ensures the layout will be correct.
	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
		mUnityPlayer.configurationChanged(newConfig);
	}

	// Notify Unity of the focus change.
	@Override
	public void onWindowFocusChanged(boolean hasFocus) {
		super.onWindowFocusChanged(hasFocus);
		mUnityPlayer.windowFocusChanged(hasFocus);
	}

	// For some reason the multiple keyevent type is not supported by the ndk.
	// Force event injection by overriding dispatchKeyEvent().
	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
			return mUnityPlayer.injectEvent(event);
		return super.dispatchKeyEvent(event);
	}

	// Pass any events not handled by (unfocused) views straight to UnityPlayer
	@Override
	public boolean onKeyUp(int keyCode, KeyEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	/* API12 */public boolean onGenericMotionEvent(MotionEvent event) {
		return mUnityPlayer.injectEvent(event);
	}

	/**
	 * 构造广播监听类，监听 SDK key 验证以及网络异常广播
	 */
	public class SDKReceiver extends BroadcastReceiver {

		public void onReceive(Context context, Intent intent) {
			String s = intent.getAction();
			Log.d("UnityPlayerActivity", " SDKReceiver action: " + s);
			// TextView text = (TextView) findViewById(R.id.verify_key_Info);
			// text.setTextColor(Color.RED);
			// if
			// (s.equals(SDKInitializer.SDK_BROADTCAST_ACTION_STRING_PERMISSION_CHECK_ERROR))
			// {
			// text.setText("key 验证出错! 错误码 :"
			// + intent.getIntExtra(
			// SDKInitializer.SDK_BROADTCAST_INTENT_EXTRA_INFO_KEY_ERROR_CODE,
			// 0) + " ; 请在 AndroidManifest.xml 文件中检查 key 设置");
			// } else if (s
			// .equals(SDKInitializer.SDK_BROADTCAST_ACTION_STRING_PERMISSION_CHECK_OK))
			// {
			// text.setText("key 验证成功! 功能可以正常使用");
			// text.setTextColor(Color.YELLOW);
			// } else if (s
			// .equals(SDKInitializer.SDK_BROADCAST_ACTION_STRING_NETWORK_ERROR))
			// {
			// text.setText("网络出错");
			// }
		}
	}

	private SDKReceiver mReceiver;

	private void initVerifyKey() {
		// TextView text = (TextView) findViewById(R.id.verify_key_Info);
		// text.setTextColor(Color.YELLOW);
		// text.setText("Android SDK v" + VersionInfo.getApiVersion());
		// 注册 SDK 广播监听者
		IntentFilter iFilter = new IntentFilter();
		iFilter.addAction(SDKInitializer.SDK_BROADTCAST_ACTION_STRING_PERMISSION_CHECK_OK);
		iFilter.addAction(SDKInitializer.SDK_BROADTCAST_ACTION_STRING_PERMISSION_CHECK_ERROR);
		iFilter.addAction(SDKInitializer.SDK_BROADCAST_ACTION_STRING_NETWORK_ERROR);
		mReceiver = new SDKReceiver();
		registerReceiver(mReceiver, iFilter);
	}
}
