package com.idealsee;

import java.security.Principal;
import java.util.ArrayList;
import java.util.List;

import android.util.Log;
import android.view.ViewDebug.FlagToString;
import android.widget.ImageView;

import com.baidu.mapapi.map.Marker;
import com.baidu.mapapi.model.LatLng;
import com.baidu.mapapi.search.route.WalkingRouteLine.WalkingStep;

/*
 * @desc   : 覆盖物的基础动画显示 
 * @author : jacky
 * @date   : 2016.9.29
 * 
 */
public class OverlayAnimationUtil implements Runnable {
	Marker marker;
	ImageView imageView;
	float fromXDelta, toXDelta, fromYDelta, toYDelta;
	public boolean moving = false;
	List<LatLng> path = new ArrayList<LatLng>();

	int currentIndex = 0;
	boolean mIncrease = true;

	public void init(ImageView imageView, float fromXDelta, float toXDelta,
			float fromYDelta, float toYDelta) {
		moving = true;
		this.imageView = imageView;
		this.fromXDelta = fromXDelta;
		this.toXDelta = toXDelta;
		this.fromYDelta = fromYDelta;
		this.toYDelta = toYDelta;
	}

	public void init(Marker marker, List<WalkingStep> list) {
		moving = true;
		this.marker = marker;
		for (int i = 0; i < list.size(); i++) {
			path.addAll(list.get(i).getWayPoints());
		}
		Log.d("OverlayAnimationUtil: size :", path.size() + "");
	}

	@Override
	public void run() {
		// Animation ani = new TranslateAnimation(fromXDelta, toXDelta,
		// fromYDelta, toYDelta);
		try {
			while (moving) {
				Thread.sleep(100);
				if (currentIndex == path.size()-1) {
					mIncrease = false;
				}
				if (currentIndex == 0) {
					mIncrease = true;
				}
				marker.setPosition(path.get(currentIndex));
//				Log.d("OverlayAnimationUtil: currentIndex: ", currentIndex + ","
//						+ path.get(currentIndex).toString());
				if (mIncrease)
					currentIndex++;
				else
					currentIndex--;
			}
		} catch (Exception e) {
			System.err.println(e.toString());
		}
	}

	public void stop() {
		Log.d("OverlayAnimationUtil:", "stop");
		moving = false;
	}
}
