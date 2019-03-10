package area.epitech.area.Services

import android.util.Log
import area.epitech.area.ViewModels.Account.LoginViewModel
import area.epitech.area.ViewModels.ConnectedViewModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.core.*
import com.github.kittinunf.fuel.core.extensions.cUrlString
import com.google.gson.Gson

class AccountService {
    private val TAG = AccountService::class.java.simpleName
    companion object {
        val instance: AccountService = AccountService()
    }

    fun connect(username: String, password: String): Request {
        val model = LoginViewModel()
        model.UserName = username
        model.Password = password
        Log.d(TAG, "Sending connection request...")
        val json = Gson().toJson(model)
        Log.d(TAG, "Model Json: $json")
        return Fuel.post("/api/account/login")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }

    fun get(token: String): Request {
        val model = ConnectedViewModel()
        model.Token = token
        Log.d(TAG, "Getting account request...")
        val json = Gson().toJson(model)
        Log.d(TAG, "Model Json: $json")
        return Fuel.post("/api/account/get")
            .body(json)
            .also { Log.d(TAG, it.cUrlString()) }

    }
}