package area.epitech.area.ViewModels

import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import java.io.Reader

class LoginViewModel {
    public var UserName: String = ""
    public var Password: String = ""

    class Deserializer : ResponseDeserializable<LoginViewModel> {
        override fun deserialize(reader: Reader) = Gson().fromJson(reader, LoginViewModel::class.java)!!
    }
}