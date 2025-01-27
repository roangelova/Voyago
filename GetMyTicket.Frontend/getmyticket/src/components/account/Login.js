import { toast, ToastContainer } from "react-toastify";
import { Account } from "../../services/accountService";
import Cookies from 'js-cookie';

function Login() {

    const Login = async (formData) => {

        let email = formData.get('email');
        let password = formData.get('password');

        if (email == '' || password == '') {
            toast.error('Emails and password required for login.')
            return;
        }

        Account.login({ email, password }).then(res => {

            console.log(res)

            let JWT = {
                accessToken: res.accessToken,
                refreshToken: res.refreshToken,
                tokenType: res.TokenType,
                accessTokenExpires: res.accessTokenExpires
            }

            //TODO - configure CORS
            //TODO -> FIX BUG -> WHY IS A PART OF THE COOKIE MISSING? COOKIE SIZE IS OK;
            Cookies.set('JWT', JSON.stringify(JWT), { expires: new Date(res.accessTokenExpires), secure: true, sameSite: false });

            toast.success('Logged in!')
        }).catch(err => {
            console.log(err)
            toast.error(err.response.data)
        })
    }

    return (
        <div className="login__form">
            <ToastContainer />
            <form action={Login}>
                <div className="login__row">
                    <label htmlFor="email" >
                        Email:
                    </label>
                    <input
                        type="text"
                        name="email"
                    >
                    </input>
                </div>

                <div className="login__row">
                    <label htmlFor="password" >
                        Password:               </label>
                    <input
                        type="password"
                        name="password"
                    >
                    </input>
                </div>

                <button
                    className="login__btn"
                    type="submit"
                >
                    Login
                </button>


            </form>
        </div>
    )
}

export default Login;