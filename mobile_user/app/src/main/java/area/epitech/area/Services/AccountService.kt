package area.epitech.area.Services

import android.arch.lifecycle.Transformations.map
import android.util.Log
import area.epitech.area.Models.Account
import area.epitech.area.ViewModels.LoginViewModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.awaitResponse
import com.github.kittinunf.fuel.core.awaitResponseResult
import com.github.kittinunf.fuel.core.extensions.cUrlString
import com.github.kittinunf.fuel.core.response
import com.github.kittinunf.fuel.httpPost
import com.google.gson.Gson

class AccountService {
    private val TAG = AccountService::class.java.simpleName
    companion object {
        val instance: AccountService = AccountService()
    }

    fun connect(model: LoginViewModel) {
        Log.d("AccountService", "Sending connection request...")
        val json = Gson().toJson(model)
        Log.d("AccountService", "Model Json: $json")
        Fuel.get("http://10.0.2.2:8080/api/area/actions").response{
            _, response, result ->
            Log.d(TAG, "test");
        }
        Fuel.post("http://10.0.2.2:8080/api/account/login")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }
            .response{
                    _, response, result ->
                Log.d(TAG, "Response found")
                Log.d(TAG, "Error code ${response.statusCode}")
                when (response.statusCode) {
                    200 -> {
                        Log.d(TAG, response.responseMessage)
                    }
                    else -> {
                        Log.e(TAG, "Error request")
                    }
                }
            }

    }
}