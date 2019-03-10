package area.epitech.area.Activitys

import android.app.Fragment
import android.content.Context
import android.content.SharedPreferences
import android.graphics.Color
import android.os.Bundle
import android.os.Handler
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.Spinner
import android.widget.TextView
import area.epitech.area.R
import area.epitech.area.Services.AreaService
import area.epitech.area.Services.SpotifyService
import area.epitech.area.Services.TwitchService
import area.epitech.area.Services.YammerService
import area.epitech.area.ViewModels.Area.ActionViewModel
import area.epitech.area.ViewModels.Area.ReactionViewModel
import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.isSuccessful
import com.github.kittinunf.fuel.core.response

/**
 * A fragment representing a list of Items.
 * Activities containing this fragment MUST implement the
 * [AddFragment.OnListFragmentInteractionListener] interface.
 */
class AddFragment : Fragment() {
    private val TAG = AllFragment::class.java.simpleName
    private val PREFS_FILENAME = "area.epitech"

    private var prefs: SharedPreferences? = null
    private var selectedAction: ActionViewModel? =  null
    private var selectedReaction: ReactionViewModel? =  null
    private var states: BooleanArray = booleanArrayOf(false, false, false, false, false, false)
    private var token: String = ""

    private var actions: List<ActionViewModel> = listOf()
    private var reactions: List<ReactionViewModel> = listOf()
    private var validReactions: MutableList<ReactionViewModel> = mutableListOf()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view: View = inflater.inflate(R.layout.fragment_add, container, false)
        Log.d(TAG, "Loading application cache...")
        this.prefs = context!!.getSharedPreferences(PREFS_FILENAME, Context.MODE_PRIVATE)
        this.token = this.prefs!!.getString("Account", "")
        val actions: Spinner = view.findViewById<Spinner>(R.id.selectActions)
        this.refreshApis()
        AreaService.instance.getActions().response(ActionViewModel.ListDeserializer()){
                _, response, resultActions ->
            if (!response.isSuccessful)
                return@response
            AreaService.instance.getReactions().response(ReactionViewModel.ListDeserializer()){
                    _, response, resultReactions ->
                if (!response.isSuccessful)
                    return@response
                val mainHandler: Handler = Handler(context.getMainLooper());

                val myRunnable: Runnable = Runnable() {
                    this.actions = resultActions.get()
                    this.reactions = resultReactions.get()
                    actions.adapter = AddActionAdapter(resultActions.get(), context, this)
                }
                mainHandler.post(myRunnable)
            }
        }
        val btnConnect: Button = view.findViewById(R.id.btnSave)
        btnConnect.setOnClickListener {
            AreaService.instance.NewTrigger(this.token, this.selectedAction!!.id, this.selectedReaction!!.id).response(ResultViewModel.Deserializer()){
                _, response, result ->
                val infos: TextView = view.findViewById<TextView>(R.id.infos)
                if (response.isSuccessful){
                    if (result.get().success) {
                        infos.setTextColor(Color.GREEN)
                        infos.text = "Votre AREA à bien été enregistré"
                    } else {
                        infos.setTextColor(Color.RED)
                        infos.text = result.get().error
                    }
                } else {
                    infos.setTextColor(Color.RED)
                    infos.text = "Une erreur sais produite"
                }
            }
        }
        return view
    }

    private fun refreshApis()
    {
        SpotifyService.instance.isAvailable(this.token).response(ResultViewModel.Deserializer()){
                _, response, result ->
            if (!response.isSuccessful)
                return@response
            this.states[0] = result.get().success
        }
        TwitchService.instance.isAvailable(this.token).response(ResultViewModel.Deserializer()){
                _, response, result ->
            if (!response.isSuccessful)
                return@response
            this.states[1] = result.get().success
        }
        YammerService.instance.isAvailable(this.token).response(ResultViewModel.Deserializer()){
                _, response, result ->
            if (!response.isSuccessful)
                return@response
            this.states[2] = result.get().success
        }
    }

    public fun actionChanged(action: ActionViewModel)
    {
        if (this.selectedAction != null && action.id == this.selectedAction!!.id)
            return;
        Log.d(TAG, "Action changed...");
        val mainHandler: Handler = Handler(context.getMainLooper());

        val myRunnable: Runnable = Runnable() {
            val changed: Boolean = this.selectedAction == null || this.selectedReaction == null || (this.selectedAction!!.service != action.service)
            if (this.selectedAction != null && (this.selectedReaction == null || this.selectedAction!!.service != this.selectedReaction!!.service)) {
                needService(false, this.selectedAction!!.service)
            }
            this.selectedAction = action
            needService(true, this.selectedAction!!.service)

            if (this.selectedReaction == null || changed) {
                var result: MutableList<ReactionViewModel> = mutableListOf()
                for (reaction in this.reactions){
                    if (reaction.service == this.selectedAction!!.service)
                        result.add(reaction)
                }
                this.validReactions = result
                this.selectedReaction = null
                val reactions: Spinner = view.findViewById<Spinner>(R.id.selectReactions)
                reactions.adapter = AddReactionAdapter(this.validReactions, context, this)
                val btnConnect: Button = view.findViewById(R.id.btnSave)
                if (this.validReactions.size == 0){
                    btnConnect.visibility = View.INVISIBLE
                } else
                    btnConnect.visibility = View.VISIBLE
            }
        }
        mainHandler.post(myRunnable)
    }

    public fun reactionChanged(reaction: ReactionViewModel)
    {
        if (this.validReactions.size == 0)
            return
        Log.d(TAG, "Reaction changed...");
        val mainHandler: Handler = Handler(context.getMainLooper());

        val myRunnable: Runnable = Runnable() {
            if (this.selectedReaction != null && this.selectedReaction!!.service != this.selectedReaction!!.service) {
                needService(false, this.selectedReaction!!.service)
            }
            this.selectedReaction = reaction
            needService(true, this.selectedReaction!!.service)
        }
        mainHandler.post(myRunnable)
    }

    private fun needService(need: Boolean, id: Int){
        if (this.states[id])
            return
        var state: Int = View.INVISIBLE
        if (need)
            state = View.VISIBLE
        when (id) {
            0 -> {
                val btn: Button = view.findViewById(R.id.btnSpotify)
                btn.visibility = state
            }
            1 -> {
                val btn: Button = view.findViewById(R.id.btnTwitch)
                btn.visibility = state
            }
            2 -> {
                val btn: Button = view.findViewById(R.id.btnYammer)
                btn.visibility = state
            }
        }
    }

    public fun getService(id: Int): String{
        when (id){
            0 -> {
                return "Spotify"
            }
            1 -> {
                return "Twitch"
            }
            2 -> {
                return "Yammer"
            }
            3 -> {
                return "Reddit"
            }
            4 -> {
                return "Steam"
            }
            5 -> {
                return "Spotify"
            }
        }
        return "Inconnu"
    }
}
