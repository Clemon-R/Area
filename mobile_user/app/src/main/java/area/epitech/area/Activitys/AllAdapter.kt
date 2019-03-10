package area.epitech.area.Activitys

import android.content.Context
import android.os.Handler
import android.support.v7.widget.RecyclerView
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageButton
import android.widget.TextView
import area.epitech.area.R
import area.epitech.area.Services.AreaService
import area.epitech.area.ViewModels.Area.ActionViewModel
import area.epitech.area.ViewModels.Area.ReactionViewModel
import area.epitech.area.ViewModels.Area.TriggerViewModel
import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.isSuccessful
import com.github.kittinunf.fuel.core.response

class AllAdapter(public var items : List<TriggerViewModel>, public var actions : List<ActionViewModel>, public var reactions : List<ReactionViewModel>, public var token: String, val context: Context) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {

    override fun getItemCount(): Int {
        return items.size
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        return ViewHolder(LayoutInflater.from(context).inflate(R.layout.fragment_all_item, parent, false))
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        val model = items[position]
        var descriptionAction: ActionViewModel? = null
        for (action in actions){
            if (action.id == model.actionId) {
                descriptionAction = action
                break;
            }
        }
        var descriptionReaction: ReactionViewModel? = null
        for (reaction in reactions){
            if (reaction.id == model.reactionId) {
                descriptionReaction = reaction
                break;
            }
        }
        val lblId = holder.itemView.findViewById<TextView>(R.id.lblId)
        val lblAction = holder.itemView.findViewById<TextView>(R.id.lblAction)
        val lblReaction = holder.itemView.findViewById<TextView>(R.id.lblReaction)
        lblId.text = model.id.toString()
        lblAction.text = descriptionAction!!.description
        lblReaction.text = descriptionReaction!!.description
        val list: ImageButton = holder.itemView.findViewById(R.id.btnDelete)
        list.setOnClickListener {
            AreaService.instance.deleteTrigger(token, model.id).response(ResultViewModel.Deserializer()){
                _, response, result ->
                if (!response.isSuccessful || !result.get().success)
                    return@response
                Log.d("AllAdapter", "Trigger "+ model.id + " as been deleted")
                AreaService.instance.getTriggers(token).response(TriggerViewModel.ListDeserializer()) {
                        _, response, result ->
                    if (!response.isSuccessful)
                        return@response
                    Log.d("AllAdapter", "Assignation des triggers")
                    val mainHandler: Handler = Handler(context.getMainLooper());

                    val myRunnable: Runnable = Runnable() {
                        items = result.get()
                        this.notifyDataSetChanged()
                    }
                    mainHandler.post(myRunnable)
                }
            }
        }
    }
}

class ViewHolder (val view: View) : RecyclerView.ViewHolder(view) {

}
