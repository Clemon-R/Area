package area.epitech.area.ViewModels.Area

import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.io.Reader

class ActionViewModel : ResultViewModel() {
    public val description: String = ""
    public val compatibilitys: List<Int> = listOf()
    public val service: Int = -1
    public val id: Int = -1

    class ListDeserializer : ResponseDeserializable<List<ActionViewModel>> {
        override fun deserialize(reader: Reader): List<ActionViewModel> {
            val type = object : TypeToken<List<ActionViewModel>>() {}.type
            return Gson().fromJson(reader, type)
        }
    }
}