package area.epitech.area.Activitys

import android.accounts.Account
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.graphics.Color
import android.net.Uri
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import area.epitech.area.R
import area.epitech.area.Services.AccountService
import area.epitech.area.Services.SpotifyService
import area.epitech.area.ViewModels.Account.AccountViewModel
import area.epitech.area.ViewModels.Account.LoginViewModel
import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.*
import com.google.gson.Gson

class MainActivity : AppCompatActivity() {
    private val TAG = MainActivity::class.java.simpleName
    private val PREFS_FILENAME = "area.epitech"

    private var prefs: SharedPreferences? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        Log.d(TAG, "Init of Fuel configuration...")
        FuelManager.instance.apply {
            basePath = "http://10.0.2.2:8080"
            baseHeaders = mapOf("Content-Type" to "application/json; charset=UTF-8")
        }

        Log.d(TAG, "Binding view...")
        val editUsername = findViewById<EditText>(R.id.editUsername)
        val editPassword = findViewById<EditText>(R.id.editPassword)
        val btnConnect = findViewById<Button>(R.id.btnConnect)
        btnConnect.setOnClickListener {
            Log.d(TAG, "Connecting to account...")
            val result: Request =  AccountService.instance.connect(editUsername.text.toString(), editPassword.text.toString())
            result.response(AccountViewModel.Deserializer()) {
                    _, response, result ->
                if (response.statusCode != 200) {
                    this.connect(response,  null)
                } else {
                    this.connect(response,  result.get())
                }
            }
        }

        val btnSpotify = findViewById<Button>(R.id.btnSpotify)
        btnSpotify.setOnClickListener {
            Log.d(TAG, "Connecting to account through spotify...")
            val intent = Intent(
                Intent.ACTION_VIEW,
                Uri.parse("https://accounts.spotify.com/authorize?client_id=a42c9625d0534a27911d6d708531b4b9&response_type=code&redirect_uri=http://127.0.0.1:8081/spotify/login")
            )
            startActivity(intent)
        }

        Log.d(TAG, "Loading application cache...")
        this.prefs = this.getSharedPreferences(PREFS_FILENAME, Context.MODE_PRIVATE)
        loadAccount()
        handleSpotifyRedirec()
    }

    private fun loadAccount()
    {
        val btnConnect = findViewById<Button>(R.id.btnConnect)
        val accountData = this.prefs!!.getString("Account", null)

        if (accountData != null) {
            Log.d(TAG, "Loading account...")
            val result: Request =  AccountService.instance.get(accountData)
            result.response(AccountViewModel.Deserializer()) {
                    _, response, result ->
                if (response.statusCode != 200) {
                    this.getAccount(response,  null)
                } else {
                    this.getAccount(response,  result.get())
                }
            }
        } else
            btnConnect.visibility = View.VISIBLE
    }

    private fun handleSpotifyRedirec()
    {
        val appLinkIntent = intent

        if (appLinkIntent.action != null && appLinkIntent.action.endsWith(".VIEW")) {
            val code = appLinkIntent.data.getQueryParameter("code")
            val params = appLinkIntent.data.toString().split('/')
            if (code != null && params.size > 3) {
                when (params[3]) {
                    "spotify" -> {
                        Log.d(TAG, "Loading account...")
                        val result: Request =  SpotifyService.instance.connectLogin(code)
                        result.response(AccountViewModel.Deserializer()) {
                                _, response, result ->
                            if (response.statusCode != 200) {
                                this.connect(response,  null)
                            } else {
                                this.connect(response,  result.get())
                            }
                        }
                    }
                }
            }
        }
    }

    private fun getAccount(response: Response, result: AccountViewModel?)
    {
        if (result != null)
            when (response.statusCode)
            {
                200 -> {
                    if (result.success) {
                        Log.d(TAG, "Request successfull")
                        goToHome(result!!.token)
                        return
                    }
                }
            }
        Log.d(TAG, "Request failed")
        val editor = this.prefs!!.edit()
        editor.remove("Account")
        editor.apply()
        val btnConnect = findViewById<Button>(R.id.btnConnect)
        runOnUiThread{
            btnConnect.visibility = View.VISIBLE
        }
    }

    private fun connect(response: Response, result: AccountViewModel?)
    {
        val infos: TextView = findViewById<TextView>(R.id.infos)
        when (response.statusCode)
        {
            200 -> {
                Log.d(TAG, "Request successfull")
                if (result!!.success) {
                    runOnUiThread {
                        infos.setTextColor(Color.GREEN)
                        infos.text = "Vous avez bien été connecté"
                    }
                    val editor = this.prefs!!.edit()
                    editor.putString("Account", result!!.token)
                    editor.apply()
                    goToHome(result!!.token)
                } else {
                    runOnUiThread {
                        infos.setTextColor(Color.RED)
                        infos.text = result.error
                    }
                }
                return
            }

            else -> {
                Log.e(TAG, "Failed to access login API");
                runOnUiThread {
                    infos.setTextColor(Color.RED)
                    infos.text = "Une erreur sais produite"
                }
                return
            }
        }
    }

    private fun fillAccount(model: AccountViewModel): area.epitech.area.Models.Account {
        var result = area.epitech.area.Models.Account()
        result.Token = model.token
        result.UserName = model.username
        return result
    }

    private fun goToHome(token: String)
    {
        Log.d(TAG, "Changing acitity to HomeActivity...")
        runOnUiThread {
            val intent = HomeActivity.newIntent(this, token)
            startActivity(intent)
        }
    }

    companion object {
        fun newIntent(context: Context): Intent {
            val intent = Intent(context,
                MainActivity::class.java)
            return intent
        }
    }
}
