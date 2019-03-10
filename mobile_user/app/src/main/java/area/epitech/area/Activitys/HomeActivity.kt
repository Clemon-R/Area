package area.epitech.area.Activitys

import android.app.Fragment
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.support.design.widget.BottomNavigationView
import android.support.v7.app.AppCompatActivity;
import android.util.Log
import android.view.View
import android.widget.Button
import area.epitech.area.R
import area.epitech.area.Services.AccountService
import area.epitech.area.ViewModels.Account.AccountViewModel
import com.github.kittinunf.fuel.core.Response
import com.github.kittinunf.fuel.core.response

import kotlinx.android.synthetic.main.activity_home.*

class HomeActivity : AppCompatActivity() {
    private val TAG = HomeActivity::class.java.simpleName
    private val PREFS_FILENAME = "area.epitech"

    private var prefs: SharedPreferences? = null
    private var account: AccountViewModel? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)
        //setSupportActionBar(toolbar)
        supportActionBar?.setDisplayHomeAsUpEnabled(false)

        Log.d(TAG, "Loading application cache...")
        this.prefs = this.getSharedPreferences(PREFS_FILENAME, Context.MODE_PRIVATE)
        fab.setOnClickListener { view ->
            /*Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                .setAction("Action", null).show()*/
            Log.d(TAG, "Deconnecting...")
            val editor: SharedPreferences.Editor = this.prefs!!.edit()
            editor.remove("Account")
            editor.apply()
            Log.d(TAG, "Going back to MainActivity...")
            val intent = MainActivity.newIntent(this)
            startActivity(intent)
        }
        this.findViewById<BottomNavigationView>(R.id.bottom_navigation).setOnNavigationItemSelectedListener { item ->
            when (item.itemId) {
                R.id.action_onglet_1 -> {
                    var home: Fragment = HomeFragment()
                    changeFrament(home)
                }
                R.id.action_onglet_2 -> {
                    var all: Fragment = AllFragment()
                    changeFrament(all)
                }
                R.id.action_onglet_3 -> {
                    var add: Fragment = AddFragment()
                    changeFrament(add)
                }
            }
            return@setOnNavigationItemSelectedListener true
        }
        val token = this.prefs!!.getString("Account", null)
        if (token != null) {
            AccountService.instance.get(token).response(AccountViewModel.Deserializer()) { _, response, result ->
                Log.d(TAG, "Request done")
                if (response.statusCode != 200) {
                    this.getAccount(response,  null)
                } else {
                    this.getAccount(response,  result.get())
                }
            }
        }
        var home: Fragment = HomeFragment()
        changeFrament(home)
    }

    private fun changeFrament(frag: Fragment)
    {
        fragmentManager.beginTransaction()
            .replace(R.id.include, frag)
            .addToBackStack(null)
            .commit()
    }

    private fun getAccount(response: Response, result: AccountViewModel?)
    {
        if (result != null)
            when (response.statusCode)
            {
                200 -> {
                    if (result.success) {
                        Log.d(TAG, "Request successfull")
                        this.account = result
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
