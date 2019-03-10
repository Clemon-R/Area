package area.epitech.area.ViewModels.Area

import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.io.Reader

class ReactionViewModel : ResultViewModel() {
    public val description: String = ""
    public val compatibility: Int = -1
    public val service: Int = -1
    public val id: Int = -1

    class ListDeserializer : ResponseDeserializable<List<ReactionViewModel>> {
        override fun deserialize(reader: Reader): List<ReactionViewModel> {
            val type = object : TypeToken<List<ReactionViewModel>>() {}.type
            return Gson().fromJson(reader, type)
        }
    }
}