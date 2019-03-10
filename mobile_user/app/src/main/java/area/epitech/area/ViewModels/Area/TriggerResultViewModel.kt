package area.epitech.area.ViewModels.Area

import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.io.Reader

class TriggerResultViewModel : ResultViewModel() {
    public var id: Int = -1
    public var actionId: Int = -1
    public var reactionId: Int = -1


    class ListDeserializer : ResponseDeserializable<List<TriggerResultViewModel>> {
        override fun deserialize(reader: Reader): List<TriggerResultViewModel> {
            val type = object : TypeToken<List<TriggerResultViewModel>>() {}.type
            return Gson().fromJson(reader, type)
        }
    }
}