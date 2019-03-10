package area.epitech.area.Activitys

import android.graphics.Color
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import area.epitech.area.Models.Account
import area.epitech.area.R
import area.epitech.area.Services.AccountService
import area.epitech.area.ViewModels.LoginViewModel
import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.FuelManager
import com.github.kittinunf.fuel.core.Request
import com.github.kittinunf.fuel.core.ResponseResultOf
import com.github.kittinunf.fuel.core.response
import kotlin.system.measureTimeMillis

class MainActivity : AppCompatActivity() {
    private val TAG = MainActivity::class.java.simpleName

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        Log.d(TAG, "Init of Fuel configuration...")
        FuelManager.instance.apply {
            basePath = "http://10.0.2.2:8080"
            baseHeaders = mapOf("Content-Type" to "application/json; charset=UTF-8")
        }

        Log.d(TAG, "Binding view...")
        val editUsername = findViewById<EditText>(R.id.editUsername);
        val editPassword = findViewById<EditText>(R.id.editPassword);
        findViewById<Button>(R.id.btnConnect).setOnClickListener {
            Log.d(TAG, "Clicked on connect")
            val account = LoginViewModel()
            account.Password = editPassword.text.toString()
            account.UserName = editUsername.text.toString()
            Log.d(TAG, "Connecting to account...")
            val result: Request =  AccountService.instance.connect(model = account)
            result.response(ResultViewModel.Deserializer()) {
                    _, response, result ->
                val infos: TextView = findViewById<TextView>(R.id.infos)
                when (response.statusCode)
                {
                    200 -> {
                        val data: ResultViewModel = result.get()
                        Log.d(TAG, "Request successfull")
                        if (data.success) {
                            infos.setTextColor(Color.GREEN)
                            infos.text = "Vous avez bien été connecté"
                        } else {
                            infos.setTextColor(Color.RED)
                            infos.text = data.error
                        }
                    }

                    else -> {
                        Log.e(TAG, "Failed to access login API");
                        infos.setTextColor(Color.RED)
                        infos.text = "Une erreur sais produite"
                    }
                }
            }
        }

    }
}
