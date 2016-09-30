package com.idealsee;

import java.util.ArrayList;
import java.util.List;

import android.util.Log;

import com.baidu.mapapi.map.Marker;
import com.baidu.mapapi.model.LatLng;
import com.baidu.mapapi.search.route.WalkingRouteLine.WalkingStep;
import com.idealsee.math.Vector3;

/*
 * @desc   : 覆盖物的基础动画显示 
 * @author : jacky
 * @date   : 2016.9.29
 * 
 */
public class OverlayAnimationUtil implements Runnable {
	public boolean mStartMoving = false;
	List<LatLng> path = new ArrayList<LatLng>();
	public List<Marker> marker = new ArrayList<Marker>();
	public List<Boolean> moving = new ArrayList<Boolean>();
	public List<Integer> currentIndex = new ArrayList<Integer>();
	public List<Boolean> mIncrease = new ArrayList<Boolean>();

	public void init(Marker marker, List<WalkingStep> list) {
		mStartMoving = true;
		moving.add(true);
		this.marker.add(marker);
		for (int i = 0; i < list.size(); i++) {
			path.addAll(list.get(i).getWayPoints());
		}
		currentIndex.add(0);
		mIncrease.add(true);
		marker.setPosition(path.get(0));
	}

	public void addMarker(Marker marker) {
		this.marker.add(marker);
		moving.add(true);
		currentIndex.add(0);
		mIncrease.add(true);
		marker.setPosition(path.get(0));
	}

	public void addWalkStep(List<WalkingStep> list) {
		for (int i = 0; i < list.size(); i++) {
			path.addAll(list.get(i).getWayPoints());
		}
	}

	@Override
	public void run() {
		// Animation ani = new TranslateAnimation(fromXDelta, toXDelta,
		// fromYDelta, toYDelta);
		try {
			while (mStartMoving) {
				Thread.sleep(20);
				for (int i = 0; i < marker.size(); i++) {
					if (currentIndex.get(i) == path.size() - 1) {
						mIncrease.set(i, false);
					}
					if (currentIndex.get(i) == 0) {
						mIncrease.set(i, true);
					}
					if (equalslatlng(marker.get(i).getPosition(),
							path.get(currentIndex.get(i)))) {
						if (mIncrease.get(i))
							currentIndex.set(i, currentIndex.get(i) + 1);
						else
							currentIndex.set(i, currentIndex.get(i) - 1);
					}
					marker.get(i).setPosition(
							moveBetweenLatlng(marker.get(i).getPosition(),
									path.get(currentIndex.get(i)), 0.1f));
				}
			}
		} catch (Exception e) {
			System.err.println(e.toString());
		}
	}

	public void stop() {
		Log.d("OverlayAnimationUtil:", "stop");
		mStartMoving = false;
	}

	public LatLng moveBetweenLatlng(LatLng from, LatLng to, float delta) {
		Vector3 f = new Vector3((float) from.latitude, (float) from.longitude);
		Vector3 t = new Vector3((float) to.latitude, (float) to.longitude);
		Vector3 r = Vector3.Lerp(f, t, delta);
		LatLng result = new LatLng(r.x, r.y);
		return result;
	}

	public boolean equalslatlng(LatLng l1, LatLng l2) {
		double rat = 0.0001d;
		// Log.d("OverlayAnimationUtil:",
		// "" + marker.getPosition() + ",curentIndex:" + currentIndex
		// + ",L1:" + (Math.abs(l1.latitude - l2.latitude))
		// + ",L2:" + (Math.abs(l1.longitude - l2.longitude)));
		if (Math.abs(l1.latitude - l2.latitude) <= rat)
			if (Math.abs(l1.longitude - l2.longitude) <= rat)
				return true;
		return false;
	}
}
