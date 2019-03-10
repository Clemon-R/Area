package area.epitech.area.ViewModels.Area

import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.io.Reader

class TriggerViewModel : ResultViewModel() {
    public var id: Int = -1
    public var actionId: Int = -1
    public var reactionId: Int = -1


    class ListDeserializer : ResponseDeserializable<List<TriggerViewModel>> {
        override fun deserialize(reader: Reader): List<TriggerViewModel> {
            val type = object : TypeToken<List<TriggerViewModel>>() {}.type
            return Gson().fromJson(reader, type)
        }
    }
}