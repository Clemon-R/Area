package area.epitech.area.Activitys

import android.app.Fragment
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import area.epitech.area.R

/**
 * A fragment representing a list of Items.
 * Activities containing this fragment MUST implement the
 * [AddFragment.OnListFragmentInteractionListener] interface.
 */
class AddFragment : Fragment() {
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_add, container, false)
    }
}
