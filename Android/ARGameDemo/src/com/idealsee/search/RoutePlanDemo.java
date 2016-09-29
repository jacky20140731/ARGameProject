package com.idealsee.search;

import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.baidu.mapapi.map.BaiduMap;
import com.baidu.mapapi.map.BitmapDescriptor;
import com.baidu.mapapi.map.MapPoi;
import com.baidu.mapapi.map.MapStatusUpdateFactory;
import com.baidu.mapapi.map.MapView;
import com.baidu.mapapi.model.LatLng;
import com.baidu.mapapi.overlayutil.BikingRouteOverlay;
import com.baidu.mapapi.overlayutil.DrivingRouteOverlay;
import com.baidu.mapapi.overlayutil.OverlayManager;
import com.baidu.mapapi.overlayutil.TransitRouteOverlay;
import com.baidu.mapapi.overlayutil.WalkingRouteOverlay;
import com.baidu.mapapi.search.core.RouteLine;
import com.baidu.mapapi.search.core.SearchResult;
import com.baidu.mapapi.search.route.BikingRouteLine;
import com.baidu.mapapi.search.route.BikingRoutePlanOption;
import com.baidu.mapapi.search.route.BikingRouteResult;
import com.baidu.mapapi.search.route.DrivingRouteLine;
import com.baidu.mapapi.search.route.DrivingRoutePlanOption;
import com.baidu.mapapi.search.route.DrivingRouteResult;
import com.baidu.mapapi.search.route.OnGetRoutePlanResultListener;
import com.baidu.mapapi.search.route.PlanNode;
import com.baidu.mapapi.search.route.RoutePlanSearch;
import com.baidu.mapapi.search.route.TransitRouteLine;
import com.baidu.mapapi.search.route.TransitRoutePlanOption;
import com.baidu.mapapi.search.route.TransitRouteResult;
import com.baidu.mapapi.search.route.WalkingRouteLine;
import com.baidu.mapapi.search.route.WalkingRoutePlanOption;
import com.baidu.mapapi.search.route.WalkingRouteResult;

/**
 * 此demo用来展示如何进行驾车、步行、公交路线搜索并在地图使用RouteOverlay、TransitOverlay绘制
 * 同时展示如何进行节点浏览并弹出泡泡
 */
public class RoutePlanDemo implements BaiduMap.OnMapClickListener,
		OnGetRoutePlanResultListener {
	boolean useDefaultIcon = false;
	private TextView popupText = null; // 泡泡view

	// 地图相关，使用继承MapView的MyRouteMapView目的是重写touch事件实现泡泡处理
	// 如果不处理touch事件，则无需继承，直接使用MapView即可
	MapView mMapView = null; // 地图View
	BaiduMap mBaidumap = null;
	// 搜索相关
	RoutePlanSearch mSearch = null; // 搜索模块，也可去掉地图模块独立使用

	TransitRouteResult nowResult = null;
	DrivingRouteResult nowResultd = null;
	int nodeIndex = -1; // 节点索引,供浏览节点时使用
	// 获得路线图
	public RouteLine route = null;
	OverlayManager routeOverlay = null;

	public void init(MapView mMapView, BaiduMap mBaidumap) {
		this.mMapView = mMapView;
		this.mBaidumap = mBaidumap;
		// 初始化地图
		// 地图点击事件处理
		this.mBaidumap.setOnMapClickListener(this);
		// 初始化搜索模块，注册事件监听
		mSearch = RoutePlanSearch.newInstance();
		mSearch.setOnGetRoutePlanResultListener(this);
	}

	/**
	 * 发起路线规划搜索示例
	 *
	 * @param v
	 */
	public void searchProcess(PlanNode stNode, PlanNode enNode, int type) {
		Log.d("RoutePlanDemo:",
				stNode.getLocation() + "," + enNode.getLocation());
		route = null;
		if (type == 0) {
			mSearch.drivingSearch((new DrivingRoutePlanOption()).from(stNode)
					.to(enNode));
		} else if (type == 1) {
			mSearch.transitSearch((new TransitRoutePlanOption()).from(stNode)
					.city("北京").to(enNode));
		} else if (type == 2) {
			mSearch.walkingSearch((new WalkingRoutePlanOption()).from(stNode)
					.to(enNode));
		} else if (type == 3) {
			mSearch.bikingSearch((new BikingRoutePlanOption()).from(stNode).to(
					enNode));
		}
	}

	/**
	 * 节点浏览示例
	 *
	 * @param v
	 */
	public void nodeClick(View v) {
		if (route == null || route.getAllStep() == null) {
			return;
		}
		// if (nodeIndex == -1 && v.getId() == R.id.pre) {
		// return;
		// }
		// // 设置节点索引
		// if (v.getId() == R.id.next) {
		// if (nodeIndex < route.getAllStep().size() - 1) {
		// nodeIndex++;
		// } else {
		// return;
		// }
		// } else if (v.getId() == R.id.pre) {
		// if (nodeIndex > 0) {
		// nodeIndex--;
		// } else {
		// return;
		// }
		// }
		// 获取节结果信息
		LatLng nodeLocation = null;
		String nodeTitle = null;
		Object step = route.getAllStep().get(nodeIndex);
		if (step instanceof DrivingRouteLine.DrivingStep) {
			nodeLocation = ((DrivingRouteLine.DrivingStep) step).getEntrance()
					.getLocation();
			nodeTitle = ((DrivingRouteLine.DrivingStep) step).getInstructions();
		} else if (step instanceof WalkingRouteLine.WalkingStep) {
			nodeLocation = ((WalkingRouteLine.WalkingStep) step).getEntrance()
					.getLocation();
			nodeTitle = ((WalkingRouteLine.WalkingStep) step).getInstructions();
		} else if (step instanceof TransitRouteLine.TransitStep) {
			nodeLocation = ((TransitRouteLine.TransitStep) step).getEntrance()
					.getLocation();
			nodeTitle = ((TransitRouteLine.TransitStep) step).getInstructions();
		} else if (step instanceof BikingRouteLine.BikingStep) {
			nodeLocation = ((BikingRouteLine.BikingStep) step).getEntrance()
					.getLocation();
			nodeTitle = ((BikingRouteLine.BikingStep) step).getInstructions();
		}

		if (nodeLocation == null || nodeTitle == null) {
			return;
		}
		// 移动节点至中心
		mBaidumap.setMapStatus(MapStatusUpdateFactory.newLatLng(nodeLocation));

	}

	/**
	 * 切换路线图标，刷新地图使其生效 注意： 起终点图标使用中心对齐.
	 */
	public void changeRouteIcon(View v) {
		if (routeOverlay == null) {
			return;
		}
		if (useDefaultIcon) {
			((Button) v).setText("自定义起终点图标");
			Log.d("RoutePlanDemo", "将使用系统起终点图标");

		} else {
			((Button) v).setText("系统起终点图标");
			Log.d("RoutePlanDemo", "将使用系统起终点图标");
		}
		useDefaultIcon = !useDefaultIcon;
		routeOverlay.removeFromMap();
		routeOverlay.addToMap();
	}

	@Override
	public void onGetWalkingRouteResult(WalkingRouteResult result) {
		Log.d("RoutePlanDemo:", result.error.toString());
		if (result == null || result.error != SearchResult.ERRORNO.NO_ERROR) {
			Log.d("RoutePlanDemo", "抱歉，未找到结果");
		}
		if (result.error == SearchResult.ERRORNO.AMBIGUOUS_ROURE_ADDR) {
			// 起终点或途经点地址有岐义，通过以下接口获取建议查询信息
			// result.getSuggestAddrInfo()
			return;
		}
		if (result.error == SearchResult.ERRORNO.NO_ERROR) {
			route = result.getRouteLines().get(0);
			WalkingRouteOverlay overlay = new MyWalkingRouteOverlay(mBaidumap);
			mBaidumap.setOnMarkerClickListener(overlay);
			routeOverlay = overlay;
			overlay.setData(result.getRouteLines().get(0));
			overlay.addToMap();
			overlay.zoomToSpan();
		}

	}

	@Override
	public void onGetTransitRouteResult(TransitRouteResult result) {

		if (result == null || result.error != SearchResult.ERRORNO.NO_ERROR) {
			Log.d("RoutePlanDemo", "抱歉，未找到结果");
		}
		if (result.error == SearchResult.ERRORNO.AMBIGUOUS_ROURE_ADDR) {
			// 起终点或途经点地址有岐义，通过以下接口获取建议查询信息
			// result.getSuggestAddrInfo()
			return;
		}
		if (result.error == SearchResult.ERRORNO.NO_ERROR) {
			nodeIndex = -1;

			if (result.getRouteLines().size() > 1) {
				nowResult = result;

			} else if (result.getRouteLines().size() == 1) {
				// 直接显示
				route = result.getRouteLines().get(0);
				TransitRouteOverlay overlay = new MyTransitRouteOverlay(
						mBaidumap);
				mBaidumap.setOnMarkerClickListener(overlay);
				routeOverlay = overlay;
				overlay.setData(result.getRouteLines().get(0));
				overlay.addToMap();
				overlay.zoomToSpan();

			} else {
				Log.d("transitresult", "结果数<0");
				return;
			}

		}
	}

	@Override
	public void onGetDrivingRouteResult(DrivingRouteResult result) {
		if (result == null || result.error != SearchResult.ERRORNO.NO_ERROR) {
			Log.d("RoutePlanDemo", "抱歉，未找到结果");
		}
		if (result.error == SearchResult.ERRORNO.AMBIGUOUS_ROURE_ADDR) {
			// 起终点或途经点地址有岐义，通过以下接口获取建议查询信息
			// result.getSuggestAddrInfo()
			return;
		}
		if (result.error == SearchResult.ERRORNO.NO_ERROR) {
			nodeIndex = -1;

			if (result.getRouteLines().size() > 1) {
				nowResultd = result;

			} else if (result.getRouteLines().size() == 1) {
				route = result.getRouteLines().get(0);
				DrivingRouteOverlay overlay = new MyDrivingRouteOverlay(
						mBaidumap);
				routeOverlay = overlay;
				mBaidumap.setOnMarkerClickListener(overlay);
				overlay.setData(result.getRouteLines().get(0));
				overlay.addToMap();
				overlay.zoomToSpan();
			}

		}
	}

	@Override
	public void onGetBikingRouteResult(BikingRouteResult bikingRouteResult) {
		if (bikingRouteResult == null
				|| bikingRouteResult.error != SearchResult.ERRORNO.NO_ERROR) {
			Log.d("RoutePlanDemo", "抱歉，未找到结果");
		}
		if (bikingRouteResult.error == SearchResult.ERRORNO.AMBIGUOUS_ROURE_ADDR) {
			// 起终点或途经点地址有岐义，通过以下接口获取建议查询信息
			// result.getSuggestAddrInfo()
			return;
		}
		if (bikingRouteResult.error == SearchResult.ERRORNO.NO_ERROR) {
			nodeIndex = -1;
			route = bikingRouteResult.getRouteLines().get(0);
			BikingRouteOverlay overlay = new MyBikingRouteOverlay(mBaidumap);
			routeOverlay = overlay;
			mBaidumap.setOnMarkerClickListener(overlay);
			overlay.setData(bikingRouteResult.getRouteLines().get(0));
			overlay.addToMap();
			overlay.zoomToSpan();
		}
	}

	// 定制RouteOverly
	private class MyDrivingRouteOverlay extends DrivingRouteOverlay {

		public MyDrivingRouteOverlay(BaiduMap baiduMap) {
			super(baiduMap);
		}

		@Override
		public BitmapDescriptor getStartMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_st);
			// }
			return null;
		}

		@Override
		public BitmapDescriptor getTerminalMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_en);
			// }
			return null;
		}
	}

	private class MyWalkingRouteOverlay extends WalkingRouteOverlay {

		public MyWalkingRouteOverlay(BaiduMap baiduMap) {
			super(baiduMap);
		}

		@Override
		public BitmapDescriptor getStartMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_st);
			// }
			return null;
		}

		@Override
		public BitmapDescriptor getTerminalMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_en);
			// }
			return null;
		}
	}

	private class MyTransitRouteOverlay extends TransitRouteOverlay {

		public MyTransitRouteOverlay(BaiduMap baiduMap) {
			super(baiduMap);
		}

		@Override
		public BitmapDescriptor getStartMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_st);
			// }
			return null;
		}

		@Override
		public BitmapDescriptor getTerminalMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_en);
			// }
			return null;
		}
	}

	private class MyBikingRouteOverlay extends BikingRouteOverlay {
		public MyBikingRouteOverlay(BaiduMap baiduMap) {
			super(baiduMap);
		}

		@Override
		public BitmapDescriptor getStartMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_st);
			// }
			return null;
		}

		@Override
		public BitmapDescriptor getTerminalMarker() {
			// if (useDefaultIcon) {
			// return BitmapDescriptorFactory.fromResource(R.drawable.icon_en);
			// }
			return null;
		}

	}

	@Override
	public void onMapClick(LatLng point) {
		mBaidumap.hideInfoWindow();
	}

	@Override
	public boolean onMapPoiClick(MapPoi poi) {
		return false;
	}

	// 响应DLg中的List item 点击
	interface OnItemInDlgClickListener {
		public void onItemClick(int position);
	}

}
