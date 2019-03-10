package area.epitech.area.Activitys

import android.annotation.SuppressLint
import android.content.Context
import android.database.DataSetObserver
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.SpinnerAdapter
import android.widget.TextView
import area.epitech.area.R
import area.epitech.area.ViewModels.Area.ReactionViewModel

@SuppressLint("ParcelCreator")
class AddReactionAdapter(val models: List<ReactionViewModel>, val context: Context, val frag: AddFragment) : SpinnerAdapter {
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        frag.reactionChanged(models[position])
        return this.getDropDownView(position, convertView, parent)
    }

    override fun registerDataSetObserver(observer: DataSetObserver?) {
    }

    override fun getItemViewType(position: Int): Int {
        return 0
    }

    override fun getItem(position: Int): Any {
        return models[position]
    }

    override fun getViewTypeCount(): Int {
        return 1;
    }

    override fun hasStableIds(): Boolean {
        return true
    }

    override fun unregisterDataSetObserver(observer: DataSetObserver?) {
    }

    override fun isEmpty(): Boolean {
        return models.size == 0
    }


    override fun getItemId(position: Int): Long {
        return position.toLong()
    }

    override fun getCount(): Int {
        return models.size
    }

    override fun getDropDownView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val model: ReactionViewModel = models[position]
        val view: View = LayoutInflater.from(context).inflate(R.layout.fragment_add_item, parent, false)
        val service: TextView = view.findViewById<TextView>(R.id.lblService)
        val description: TextView = view.findViewById<TextView>(R.id.lblDescription)
        service.text = frag.getService(model.service)
        description.text = model.description
        return view
    }
}