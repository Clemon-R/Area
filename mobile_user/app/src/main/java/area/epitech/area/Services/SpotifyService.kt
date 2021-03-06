package area.epitech.area.Services

import android.util.Log
import area.epitech.area.ViewModels.ConnectedViewModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.Request
import com.github.kittinunf.fuel.core.extensions.cUrlString
import com.google.gson.Gson

class SpotifyService {
    private val TAG = SpotifyService::class.java.simpleName
    companion object {
        val instance: SpotifyService = SpotifyService()
    }


    fun connectLogin(code: String): Request {
        Log.d(TAG, "Getting account request...")
        return Fuel.get("/api/spotify/login/$code")
            .also { Log.d(TAG, it.cUrlString()) }

    }

    fun isAvailable(token: String): Request {
        val model = ConnectedViewModel()
        model.Token = token
        Log.d(TAG, "Getting spotify available request...")
        val json = Gson().toJson(model)
        Log.d(TAG, "Model Json: $json")
        return Fuel.post("/api/spotify/available/")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }
}