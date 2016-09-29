package com.idealsee;

import java.util.ArrayList;

import android.content.Context;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.SeekBar;

import com.baidu.mapapi.map.BaiduMap;
import com.baidu.mapapi.map.BaiduMap.OnMarkerClickListener;
import com.baidu.mapapi.map.BaiduMap.OnMarkerDragListener;
import com.baidu.mapapi.map.BitmapDescriptor;
import com.baidu.mapapi.map.BitmapDescriptorFactory;
import com.baidu.mapapi.map.GroundOverlayOptions;
import com.baidu.mapapi.map.InfoWindow;
import com.baidu.mapapi.map.InfoWindow.OnInfoWindowClickListener;
import com.baidu.mapapi.map.MapStatusUpdate;
import com.baidu.mapapi.map.MapStatusUpdateFactory;
import com.baidu.mapapi.map.MapView;
import com.baidu.mapapi.map.Marker;
import com.baidu.mapapi.map.MarkerOptions;
import com.baidu.mapapi.map.MarkerOptions.MarkerAnimateType;
import com.baidu.mapapi.map.OverlayOptions;
import com.baidu.mapapi.model.LatLng;
import com.baidu.mapapi.model.LatLngBounds;
import com.baidu.mapapi.search.core.RouteNode;
import com.baidu.mapapi.search.route.PlanNode;

/**
 * 演示覆盖物的用法
 */
public class OverlayDemo {

	/**
	 * MapView 是地图主控件
	 */
	private MapView mMapView;
	private BaiduMap mBaiduMap;
	private Context context;
	public Marker mMarkerA;
	public Marker mMarkerB;
	public Marker mMarkerC;
	public Marker mMarkerD;
	public InfoWindow mInfoWindow;

	// 初始化全局 bitmap 信息，不用时及时 recycle
	BitmapDescriptor bdA = BitmapDescriptorFactory
			.fromResource(R.drawable.icon_marka);
	BitmapDescriptor bdB = BitmapDescriptorFactory
			.fromResource(R.drawable.icon_markb);
	BitmapDescriptor bdC = BitmapDescriptorFactory
			.fromResource(R.drawable.icon_markc);
	BitmapDescriptor bdD = BitmapDescriptorFactory
			.fromResource(R.drawable.icon_markd);
	BitmapDescriptor bd = BitmapDescriptorFactory
			.fromResource(R.drawable.icon_gcoding);
	BitmapDescriptor bdGround = BitmapDescriptorFactory
			.fromResource(R.drawable.ground_overlay);

	public void init(final Context context, MapView mapView, BaiduMap baiduMap) {
		mMapView = mapView;
		mBaiduMap = baiduMap;
		this.context = context;
		// MapStatusUpdate msu = MapStatusUpdateFactory.zoomTo(14.0f);
		// mBaiduMap.setMapStatus(msu);
		// initOverlay();
		initOverlayListener();
	}

	public void initOverlayListener() {
		mBaiduMap.setOnMarkerClickListener(new OnMarkerClickListener() {
			public boolean onMarkerClick(final Marker marker) {
				Button button = new Button(context);
				button.setBackgroundResource(R.drawable.popup);
				OnInfoWindowClickListener listener = null;
				if (marker == mMarkerA || marker == mMarkerD) {
					button.setText("更改位置");
					listener = new OnInfoWindowClickListener() {
						public void onInfoWindowClick() {
							LatLng ll = marker.getPosition();
							LatLng llNew = new LatLng(ll.latitude + 0.005,
									ll.longitude + 0.005);
							marker.setPosition(llNew);
							mBaiduMap.hideInfoWindow();
						}
					};
					LatLng ll = marker.getPosition();
					mInfoWindow = new InfoWindow(BitmapDescriptorFactory
							.fromView(button), ll, -47, listener);
					mBaiduMap.showInfoWindow(mInfoWindow);
				} else if (marker == mMarkerB) {
					button.setText("更改图标");
					button.setOnClickListener(new OnClickListener() {
						public void onClick(View v) {
							marker.setIcon(bd);
							mBaiduMap.hideInfoWindow();
						}
					});
					LatLng ll = marker.getPosition();
					mInfoWindow = new InfoWindow(button, ll, -47);
					mBaiduMap.showInfoWindow(mInfoWindow);
				} else if (marker == mMarkerC) {
					button.setText("删除");
					button.setOnClickListener(new OnClickListener() {
						public void onClick(View v) {
							marker.remove();
							mBaiduMap.hideInfoWindow();
						}
					});
					LatLng ll = marker.getPosition();
					mInfoWindow = new InfoWindow(button, ll, -47);
					mBaiduMap.showInfoWindow(mInfoWindow);
				}
				return true;
			}
		});
	}

	public void addOverlay(RouteNode node, MarkerAnimateType animationType) {
		Log.d("OverlayDemo:", node.getTitle());
		MarkerOptions ooA = new MarkerOptions().position(node.getLocation())
				.icon(bdA).zIndex(9).draggable(true);
		if (animationType == MarkerAnimateType.drop) {
			ooA.animateType(MarkerAnimateType.drop);
		} else if (animationType == MarkerAnimateType.grow) {
			ooA.animateType(MarkerAnimateType.grow);
		}
		mMarkerA = (Marker) (mBaiduMap.addOverlay(ooA));
	}

	public void initOverlay() {
		// add marker overlay
		LatLng llA = new LatLng(39.963175, 116.400244);
		LatLng llB = new LatLng(39.942821, 116.369199);
		LatLng llC = new LatLng(39.939723, 116.425541);
		LatLng llD = new LatLng(39.906965, 116.401394);

		MarkerOptions ooA = new MarkerOptions().position(llA).icon(bdA)
				.zIndex(9).draggable(true);
		// if (animationBox.isChecked()) {
		// // 掉下动画
		ooA.animateType(MarkerAnimateType.drop);
		// }
		mMarkerA = (Marker) (mBaiduMap.addOverlay(ooA));
		MarkerOptions ooB = new MarkerOptions().position(llB).icon(bdB)
				.zIndex(5);
		// if (animationBox.isChecked()) {
		// // 掉下动画
		ooB.animateType(MarkerAnimateType.drop);
		// }
		mMarkerB = (Marker) (mBaiduMap.addOverlay(ooB));
		MarkerOptions ooC = new MarkerOptions().position(llC).icon(bdC)
				.perspective(false).anchor(0.5f, 0.5f).rotate(30).zIndex(7);
		// if (animationBox.isChecked()) {
		// // 生长动画
		ooC.animateType(MarkerAnimateType.grow);
		// }
		mMarkerC = (Marker) (mBaiduMap.addOverlay(ooC));
		ArrayList<BitmapDescriptor> giflist = new ArrayList<BitmapDescriptor>();
		giflist.add(bdA);
		giflist.add(bdB);
		giflist.add(bdC);
		MarkerOptions ooD = new MarkerOptions().position(llD).icons(giflist)
				.zIndex(0).period(10);
		// if (animationBox.isChecked()) {
		// // 生长动画
		ooD.animateType(MarkerAnimateType.grow);
		// }
		mMarkerD = (Marker) (mBaiduMap.addOverlay(ooD));

		// add ground overlay
		LatLng southwest = new LatLng(39.92235, 116.380338);
		LatLng northeast = new LatLng(39.947246, 116.414977);
		LatLngBounds bounds = new LatLngBounds.Builder().include(northeast)
				.include(southwest).build();

		OverlayOptions ooGround = new GroundOverlayOptions()
				.positionFromBounds(bounds).image(bdGround).transparency(0.8f);
		mBaiduMap.addOverlay(ooGround);

		MapStatusUpdate u = MapStatusUpdateFactory
				.newLatLng(bounds.getCenter());
		mBaiduMap.setMapStatus(u);

		mBaiduMap.setOnMarkerDragListener(new OnMarkerDragListener() {
			public void onMarkerDrag(Marker marker) {
			}

			public void onMarkerDragEnd(Marker marker) {
				Log.d("OverlayDemo:", marker.getTitle());
				// Toast.makeText(
				// OverlayDemo.this,
				// "拖拽结束，新位置：" + marker.getPosition().latitude + ", "
				// + marker.getPosition().longitude,
				// Toast.LENGTH_LONG).show();
			}

			public void onMarkerDragStart(Marker marker) {
			}
		});
	}

	/**
	 * 清除所有Overlay
	 *
	 * @param view
	 */
	public void clearOverlay(View view) {
		mBaiduMap.clear();
		mMarkerA = null;
		mMarkerB = null;
		mMarkerC = null;
		mMarkerD = null;
	}

	/**
	 * 重新添加Overlay
	 *
	 * @param view
	 */
	public void resetOverlay(View view) {
		clearOverlay(null);
		initOverlay();
	}

	private class SeekBarListener implements SeekBar.OnSeekBarChangeListener {

		@Override
		public void onProgressChanged(SeekBar seekBar, int progress,
				boolean fromUser) {
			// TODO Auto-generated method stub
			float alpha = ((float) seekBar.getProgress()) / 10;
			if (mMarkerA != null) {
				mMarkerA.setAlpha(alpha);
			}
			if (mMarkerB != null) {
				mMarkerB.setAlpha(alpha);
			}
			if (mMarkerC != null) {
				mMarkerC.setAlpha(alpha);
			}
			if (mMarkerD != null) {
				mMarkerD.setAlpha(alpha);
			}
		}

		@Override
		public void onStartTrackingTouch(SeekBar seekBar) {
			// TODO Auto-generated method stub
		}

		@Override
		public void onStopTrackingTouch(SeekBar seekBar) {
			// TODO Auto-generated method stub
		}

	}

	public void reset() {
		// 回收 bitmap 资源
		bdA.recycle();
		bdB.recycle();
		bdC.recycle();
		bdD.recycle();
		bd.recycle();
		bdGround.recycle();
	}

}
