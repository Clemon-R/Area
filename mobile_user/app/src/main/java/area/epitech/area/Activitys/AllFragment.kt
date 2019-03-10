package area.epitech.area.Activitys

import android.app.Fragment
import android.content.Context
import android.content.SharedPreferences
import android.os.Bundle
import android.os.Handler
import android.support.v7.widget.LinearLayoutManager
import android.support.v7.widget.RecyclerView
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import area.epitech.area.R
import area.epitech.area.Services.AreaService
import area.epitech.area.ViewModels.Area.ActionViewModel
import area.epitech.area.ViewModels.Area.ReactionViewModel
import area.epitech.area.ViewModels.Area.TriggerResultViewModel
import com.github.kittinunf.fuel.core.isSuccessful
import com.github.kittinunf.fuel.core.response

class AllFragment : Fragment() {
    private val TAG = AllFragment::class.java.simpleName
    private val PREFS_FILENAME = "area.epitech"

    private var prefs: SharedPreferences? = null

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view: View = inflater.inflate(R.layout.fragment_all, container, false)
        Log.d(TAG, "Loading application cache...")
        this.prefs = context!!.getSharedPreferences(PREFS_FILENAME, Context.MODE_PRIVATE)
        val token = this.prefs!!.getString("Account", null)
        val list: RecyclerView = view.findViewById(R.id.listTriggers)
        if (token != null)
            AreaService.instance.getActions().response(ActionViewModel.ListDeserializer()) {
                    _, response, actions ->
                if (!response.isSuccessful)
                    return@response
                AreaService.instance.getReactions().response(ReactionViewModel.ListDeserializer()) {
                        _, response, reactions ->
                    if (!response.isSuccessful)
                        return@response
                    AreaService.instance.getTriggers(token).response(TriggerResultViewModel.ListDeserializer()) {
                            _, response, result ->
                        if (!response.isSuccessful)
                            return@response
                        Log.d(TAG, "Assignation des triggers")
                        val adapter = list.adapter as AllAdapter
                        val mainHandler: Handler  = Handler(context.getMainLooper());

                        val myRunnable: Runnable = Runnable() {
                            adapter.actions = actions.get()
                            adapter.reactions = reactions.get()
                            adapter.items = result.get()
                            adapter.token = token
                            adapter.notifyDataSetChanged()
                        }
                        mainHandler.post(myRunnable)
                    }
                }
            }
        list.layoutManager = LinearLayoutManager(context)
        list.adapter = AllAdapter(listOf(), listOf(), listOf(), "", context)
        return view
    }
}
