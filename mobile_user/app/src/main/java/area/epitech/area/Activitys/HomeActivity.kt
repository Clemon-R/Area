package area.epitech.area.Activitys

import android.accounts.Account
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.support.design.widget.Snackbar
import android.support.v7.app.AppCompatActivity;
import android.util.Log
import android.view.View
import android.widget.Button
import area.epitech.area.R
import area.epitech.area.Services.AccountService
import area.epitech.area.ViewModels.Account.AccountViewModel
import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.Request
import com.github.kittinunf.fuel.core.Response
import com.github.kittinunf.fuel.core.response
import com.google.gson.Gson

import kotlinx.android.synthetic.main.activity_home.*

class HomeActivity : AppCompatActivity() {
    private val TAG = MainActivity::class.java.simpleName
    private val PREFS_FILENAME = "area.epitech"

    private var prefs: SharedPreferences? = null
    private var account: area.epitech.area.Models.Account? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)
        //setSupportActionBar(toolbar)

        fab.setOnClickListener { view ->
            Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                .setAction("Action", null).show()
        }
        supportActionBar?.setDisplayHomeAsUpEnabled(false)
        Log.d(TAG, "Loading application cache...")
        this.prefs = this.getSharedPreferences(PREFS_FILENAME, Context.MODE_PRIVATE)
    }

    companion object {
        private val INTENT_USER_TOKEN = "user_token"
        fun newIntent(context: Context?, token: String): Intent {
            val intent = Intent(context,
                HomeActivity::class.java)
            intent.putExtra(INTENT_USER_TOKEN, token)
            return intent
        }
    }

}
