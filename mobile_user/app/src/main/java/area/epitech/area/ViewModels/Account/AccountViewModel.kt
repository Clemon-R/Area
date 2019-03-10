package area.epitech.area.ViewModels.Account

import area.epitech.area.ViewModels.ResultViewModel
import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson
import java.io.Reader

class AccountViewModel : ResultViewModel() {
    public val token = ""
    public val username = ""

    class Deserializer : ResponseDeserializable<AccountViewModel> {
        override fun deserialize(reader: Reader) = Gson().fromJson(reader, AccountViewModel::class.java)!!
    }
}