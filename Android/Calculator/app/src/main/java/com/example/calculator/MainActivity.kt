package com.example.calculator

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import kotlinx.android.synthetic.main.activity_main.*
import net.objecthunter.exp4j.ExpressionBuilder
import java.lang.ArithmeticException
import java.lang.Exception

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        btn_0.setOnClickListener{setTextFields("0")}
        btn_1.setOnClickListener{setTextFields("1")}
        btn_2.setOnClickListener{setTextFields("2")}
        btn_3.setOnClickListener{setTextFields("3")}
        btn_4.setOnClickListener{setTextFields("4")}
        btn_5.setOnClickListener{setTextFields("5")}
        btn_6.setOnClickListener{setTextFields("6")}
        btn_7.setOnClickListener{setTextFields("7")}
        btn_8.setOnClickListener{setTextFields("8")}
        btn_9.setOnClickListener{setTextFields("9")}
        minus_btn.setOnClickListener{setTextFields("-")}
        plus_btn.setOnClickListener{setTextFields("+")}
        mult_btn.setOnClickListener{setTextFields("*")}
        div_btn.setOnClickListener{setTextFields("/")}
        right_btn.setOnClickListener{setTextFields(")")}
        left_btn.setOnClickListener{setTextFields("(")}
        btn_dot.setOnClickListener{setTextFields(".")}
        ac_btn.setOnClickListener{
            math_operation.text = ""
            result_text.text = ""
        }
        back_btn.setOnClickListener{
            var str = math_operation.text.toString()
            if(str.isNotEmpty()){
                math_operation.text = str.substring(0, str.length - 1)
                result_text.text = ""
            }
        }
        btn_equals.setOnClickListener {
            try {
                val ex = ExpressionBuilder(math_operation.text.toString()).build()
                val result = ex.evaluate()
                val longres = result.toLong()
                if (result == longres.toDouble())
                    result_text.text = longres.toString()
                else
                    result_text.text = result.toString()
            } catch (e:Exception){
                Log.d("Ошибка", "сообщение: ${e.message}")
                result_text.text = "Ошибка"
            }
        }
    }

    fun setTextFields(str: String){
        if (result_text.text != "") {
            math_operation.text = result_text.text
            result_text.text = ""
        }

        math_operation.append(str)
    }
}