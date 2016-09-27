package com.idealsee;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import com.baidu.location.BDLocation;
import com.baidu.location.BDLocationListener;
import com.baidu.location.LocationClient;
import com.baidu.location.LocationClientOption;
import com.baidu.mapapi.map.BaiduMap;
import com.baidu.mapapi.map.MapStatus;
import com.baidu.mapapi.map.MapStatusUpdateFactory;
import com.baidu.mapapi.map.MapView;
import com.baidu.mapapi.map.MyLocationConfiguration.LocationMode;
import com.baidu.mapapi.map.MyLocationData;
import com.baidu.mapapi.model.LatLng;

/**
 * 演示MapView的基本用法
 */
public class BaseMapDemo extends Activity {

	@SuppressWarnings("unused")
	private static final String LTAG = BaseMapDemo.class.getSimpleName();
	private MapView mMapView;
	private BaiduMap mBaiduMap;
	LocationClient mLocClient;
	public MyLocationListenner myListener = new MyLocationListenner();
	private LocationMode mCurrentMode;
	boolean isFirstLoc = true; // 是否首次定位

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_basemap);
		initView(this);
	}

	// 初始化View
	public void initView(Context context) {
		MapView.setMapCustomEnable(true);
		setMapCustomFile(this);
		Button rButton = (Button) findViewById(R.id.returnMainButton);

		rButton.setOnClickListener(new Button.OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent intent = new Intent(BaseMapDemo.this,
						UnityPlayerActivity.class);
				intent.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
				startActivity(intent);
			}
		});
		mMapView = (MapView) findViewById(R.id.bmapView);
		mBaiduMap = mMapView.getMap();
		// 开启定位图层
		mBaiduMap.setMyLocationEnabled(true);
		// 定位初始化
		mLocClient = new LocationClient(this);
		mLocClient.registerLocationListener(myListener);
		LocationClientOption option = new LocationClientOption();
		option.setOpenGps(true); // 打开gps
		option.setCoorType("bd09ll"); // 设置坐标类型
		option.setScanSpan(1000);
		mLocClient.setLocOption(option);
		mLocClient.start();
		Log.d(LTAG, ">>>" + mLocClient.getAccessKey());
	}

	// 设置个性化地图config文件路径
	private void setMapCustomFile(Context context) {
		FileOutputStream out = null;
		InputStream inputStream = null;
		String moduleName = null;
		try {
			inputStream = context.getAssets().open(
					"customConfigdir/custom_config.txt");
			byte[] b = new byte[inputStream.available()];
			inputStream.read(b);

			moduleName = context.getFilesDir().getAbsolutePath();
			File f = new File(moduleName + "/" + "custom_config.txt");
			if (f.exists()) {
				f.delete();
			}
			f.createNewFile();
			out = new FileOutputStream(f);
			out.write(b);
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			try {
				if (inputStream != null) {
					inputStream.close();
				}
				if (out != null) {
					out.close();
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
		}

		MapView.setCustomMapStylePath(moduleName + "/custom_config.txt");

	}

	@Override
	protected void onPause() {
		super.onPause();
		// activity 暂停时同时暂停地图控件
		// mMapView.onPause();
	}

	@Override
	protected void onResume() {
		super.onResume();
		// activity 恢复时同时恢复地图控件
		// mMapView.onResume();
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		mLocClient.stop();
		// 关闭定位图层
		mBaiduMap.setMyLocationEnabled(false);
		// activity 销毁时同时销毁地图控件
		mMapView.onDestroy();
		MapView.setMapCustomEnable(false);
	}

	/**
	 * 定位SDK监听函数
	 */
	public class MyLocationListenner implements BDLocationListener {

		@Override
		public void onReceiveLocation(BDLocation location) {
			// map view 销毁后不在处理新接收的位置
			if (location == null || mMapView == null) {
				return;
			}
			MyLocationData locData = new MyLocationData.Builder()
					.accuracy(location.getRadius())
					// 此处设置开发者获取到的方向信息，顺时针0-360
					.direction(100).latitude(location.getLatitude())
					.longitude(location.getLongitude()).build();
			mBaiduMap.setMyLocationData(locData);
			if (isFirstLoc) {
				isFirstLoc = false;
				LatLng ll = new LatLng(location.getLatitude(),
						location.getLongitude());
				MapStatus.Builder builder = new MapStatus.Builder();
				builder.target(ll).zoom(18.0f);
				mBaiduMap.animateMapStatus(MapStatusUpdateFactory
						.newMapStatus(builder.build()));
			}
		}

		public void onReceivePoi(BDLocation poiLocation) {
		}
	}

}
