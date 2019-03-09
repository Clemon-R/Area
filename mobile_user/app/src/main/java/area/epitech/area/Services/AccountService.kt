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
        Fuel.post("http://127.0.0.1:8080/api/acocunt/login")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }
            .header(mapOf("Content-Type" to "application/json; charset=UTF-8"))
            .responseObject(LoginViewModel.Deserializer()){
                    _, response, result ->
                Log.d(TAG, "Response found")
                Log.d(TAG, response.responseMessage)
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