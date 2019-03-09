package area.epitech.area.Activitys

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import area.epitech.area.Models.Account
import area.epitech.area.R
import area.epitech.area.Services.AccountService
import area.epitech.area.ViewModels.LoginViewModel
import com.github.kittinunf.fuel.core.FuelManager
import kotlin.system.measureTimeMillis

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        FuelManager.instance.apply {
            baseHeaders = mapOf("Content-Type" to "Application/json")
        }
        val editUsername = findViewById<EditText>(R.id.editUsername);
        val editPassword = findViewById<EditText>(R.id.editPassword);
        findViewById<Button>(R.id.btnConnect).setOnClickListener {
            Log.d("MainActivity", "Clicked on connect")
            val account = LoginViewModel()
            account.Password = editPassword.text.toString()
            account.UserName = editUsername.text.toString()
            Log.d("MainActivity", "Connecting to account...")
            AccountService.instance.connect(model = account)
        }

    }
}
