package area.epitech.area.Services

import android.arch.lifecycle.Transformations.map
import android.util.Log
import area.epitech.area.Models.Account
import area.epitech.area.ViewModels.LoginViewModel
import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.*
import com.github.kittinunf.fuel.core.extensions.cUrlString
import com.github.kittinunf.fuel.httpPost
import com.google.gson.Gson

class AccountService {
    private val TAG = AccountService::class.java.simpleName
    companion object {
        val instance: AccountService = AccountService()
    }

    fun connect(model: LoginViewModel): Request {
        Log.d("AccountService", "Sending connection request...")
        val json = Gson().toJson(model)
        Log.d("AccountService", "Model Json: $json")
        return Fuel.post("/api/account/login")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }
}