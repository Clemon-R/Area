package area.epitech.area.ViewModels

import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import java.io.Reader

open class ResultViewModel {
    public val success: Boolean = false
    public val error: String = ""

    class Deserializer : ResponseDeserializable<ResultViewModel> {
        override fun deserialize(reader: Reader) = Gson().fromJson(reader, ResultViewModel::class.java)!!
    }
}