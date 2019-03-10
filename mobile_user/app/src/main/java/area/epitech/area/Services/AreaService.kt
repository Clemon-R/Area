package area.epitech.area.Services

import android.util.Log
import area.epitech.area.ViewModels.ConnectedViewModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.Request
import com.github.kittinunf.fuel.core.extensions.cUrlString
import com.google.gson.Gson

class AreaService {
    private val TAG = AreaService::class.java.simpleName
    companion object {
        val instance: AreaService = AreaService()
    }


    fun deleteTrigger(token: String, id: Int): Request {
        val model = ConnectedViewModel()
        model.Token = token
        Log.d(TAG, "Getting triggers request...")
        val json = Gson().toJson(model)
        Log.d(TAG, "Model Json: $json")
        return Fuel.post("/api/area/delete/trigger/" + id)
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }

    fun getTriggers(token: String): Request {
        val model = ConnectedViewModel()
        model.Token = token
        Log.d(TAG, "Getting triggers request...")
        val json = Gson().toJson(model)
        Log.d(TAG, "Model Json: $json")
        return Fuel.post("/api/area/triggers")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }

    fun getActions(): Request {
        Log.d(TAG, "Getting actions request...")
        return Fuel.get("/api/area/actions")
            .also { Log.d(TAG, it.cUrlString()) }

    }

    fun getReactions(): Request {
        Log.d(TAG, "Getting reactions request...")
        return Fuel.get("/api/area/reactions")
            .also { Log.d(TAG, it.cUrlString()) }

    }
}