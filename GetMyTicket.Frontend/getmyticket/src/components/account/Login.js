import { toast, ToastContainer } from "react-toastify";
import { Account } from "../../services/accountService";
import Cookies from 'js-cookie';

function Login() {

    const Login = (formData) => {

        let email = formData.get('email');
        let password = formData.get('password');

        if (email == '' || password == '') {
            toast.error('Emails and password required for login.')
            return;
        }

        Account.login({ email, password }).then(res => {

            //TODO - configure CORS

            Cookies.set('accessToken',
                JSON.stringify(
                    {
                        accessToken: res.accessToken,
                        tokenType: res.tokenType
                    }),
                {
                    expires: new Date(res.accessTokenExpires),
                    secure: true, sameSite: false
                });

            Cookies.set('refreshToken',
                JSON.stringify(
                    { refreshToken: res.refreshToken }),
                {
                    expires: new Date(res.refreshTokenExpires),
                    secure: true, sameSite: false
                });

            toast.success('Logged in!')

            setTimeout(() => {
                window.location.href = "/";
            }
                , 3000);
        }).catch(err => {
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