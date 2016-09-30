package com.idealsee;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.List;

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
import com.baidu.mapapi.map.MapStatusUpdate;
import com.baidu.mapapi.map.MapStatusUpdateFactory;
import com.baidu.mapapi.map.MapView;
import com.baidu.mapapi.map.MyLocationConfiguration;
import com.baidu.mapapi.map.MarkerOptions.MarkerAnimateType;
import com.baidu.mapapi.map.MyLocationConfiguration.LocationMode;
import com.baidu.mapapi.map.MyLocationData;
import com.baidu.mapapi.model.LatLng;
import com.baidu.mapapi.search.core.RouteNode;
import com.baidu.mapapi.search.route.PlanNode;
import com.baidu.mapapi.search.route.WalkingRouteLine;
import com.baidu.mapapi.search.route.WalkingRouteLine.WalkingStep;
import com.idealsee.search.RoutePlanDemo;

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
	boolean isFirstLoc = true; // 是否首次定位

	Button refreshButton;
	Button flushButton;
	Button locateButton;

	// 目前先暂时使用四个朝向，获取以自己为中心点，东南西北各一个朝向的步行街道路线图
	RoutePlanDemo[] routePlanDemo = new RoutePlanDemo[4];
	// 偏移的经纬度的单位
	float mRange = 0.002f;
	// 添加覆盖物
	OverlayDemo overlayDemo;
	Thread overlayMove;
	OverlayAnimationUtil util;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_basemap);
		initView(this);
		overlayDemo = new OverlayDemo();
		overlayDemo.init(this, mMapView, mBaiduMap);
	}

	// 初始化View
	public void initView(Context context) {
		MapView.setMapCustomEnable(true);
		setMapCustomFile(this);
		refreshButton = (Button) findViewById(R.id.refresh);
		refreshButton.setOnClickListener(new Button.OnClickListener() {
			@Override
			public void onClick(View v) {
				LatLng ll = mBaiduMap.getMapStatus().target;
				PlanNode stNode = PlanNode.withLocation(ll);
				PlanNode enNode = PlanNode.withLocation(new LatLng(ll.latitude,
						ll.longitude + mRange));
				routePlanDemo[0].searchProcess(stNode, enNode, 2);

				stNode = PlanNode.withLocation(ll);
				enNode = PlanNode.withLocation(new LatLng(ll.latitude,
						ll.longitude - mRange));
				routePlanDemo[1].searchProcess(stNode, enNode, 2);

				stNode = PlanNode.withLocation(ll);
				enNode = PlanNode.withLocation(new LatLng(ll.latitude + mRange,
						ll.longitude));
				routePlanDemo[2].searchProcess(stNode, enNode, 2);

				stNode = PlanNode.withLocation(ll);
				enNode = PlanNode.withLocation(new LatLng(ll.latitude - mRange,
						ll.longitude));
				routePlanDemo[3].searchProcess(stNode, enNode, 2);

				if (util != null)
					util.stop();
				util = null;
				if (overlayMove != null)
					overlayMove.interrupt();
			}
		});
		flushButton = (Button) findViewById(R.id.flush);
		flushButton.setOnClickListener(new Button.OnClickListener() {
			@Override
			public void onClick(View v) {
				RouteNode node = routePlanDemo[0].route.getStarting();
				overlayDemo.addOverlay(node, MarkerAnimateType.none);
				if (util == null) {
					util = new OverlayAnimationUtil();
					util.init(overlayDemo.mMarkerA,
							routePlanDemo[0].route.getAllStep());
					for (int i = 1; i < routePlanDemo.length; i++) {
						util.addWalkStep(routePlanDemo[i].route.getAllStep());
					}
					overlayMove = new Thread(util);
					overlayMove.start();
				} else {
					util.addMarker(overlayDemo.mMarkerA);
				}
			}
		});

		locateButton = (Button) findViewById(R.id.locate);
		locateButton.setOnClickListener(new Button.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (locateButton.getText().equals("normal")) {
					locateButton.setText("locate");
					mBaiduMap
							.setMyLocationConfigeration(new MyLocationConfiguration(
									LocationMode.NORMAL, true, null));
				} else if (locateButton.getText().equals("locate")) {
					locateButton.setText("normal");
					mBaiduMap
							.setMyLocationConfigeration(new MyLocationConfiguration(
									LocationMode.FOLLOWING, true, null));
				}
			}
		});

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
		if (util != null)
			util.stop();
		if (overlayMove != null)
			overlayMove.interrupt();
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

				// MapStatus mMapStatus = new MapStatus.Builder(
				// mBaiduMap.getMapStatus()).zoom(19.5f).overlook(-80)
				// .build();
				// MapStatusUpdate msu = MapStatusUpdateFactory
				// .newMapStatus(mMapStatus);
				// mBaiduMap.animateMapStatus(msu);

				LatLng ll = new LatLng(location.getLatitude(),
						location.getLongitude());
				MapStatus.Builder builder = new MapStatus.Builder();
				builder.target(ll).zoom(19.5f).overlook(-80);
				mBaiduMap.animateMapStatus(MapStatusUpdateFactory
						.newMapStatus(builder.build()));

				for (int i = 0; i < routePlanDemo.length; i++) {
					routePlanDemo[i] = new RoutePlanDemo();
					routePlanDemo[i].init(mMapView, mBaiduMap);
				}
			}
		}

		public void onReceivePoi(BDLocation poiLocation) {
		}
	}

}
