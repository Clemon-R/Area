package area.epitech.area.Services

import android.util.Log
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.Request
import com.github.kittinunf.fuel.core.extensions.cUrlString

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
}