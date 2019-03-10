package area.epitech.area.Services

import android.util.Log
import area.epitech.area.ViewModels.ConnectedViewModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.Request
import com.github.kittinunf.fuel.core.extensions.cUrlString
import com.google.gson.Gson

class TwitchService {
    private val TAG = TwitchService::class.java.simpleName
    companion object {
        val instance: TwitchService = TwitchService()
    }


    fun isAvailable(token: String): Request {
        val model = ConnectedViewModel()
        model.Token = token
        Log.d(TAG, "Getting twitch available request...")
        val json = Gson().toJson(model)
        Log.d(TAG, "Model Json: $json")
        return Fuel.post("/api/twitch/available/")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }
}