import { toast, ToastContainer } from "react-toastify";
import { Account } from "../../services/accountService";
import Cookies from 'js-cookie';

function Login({ setLoginPopupVisibility }) {

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

            toast.success('Logged in! Taking you to the homepage in 3, 2, 1..')

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
            <div className="login__iconBox">
                <svg
                    onClick={() => setLoginPopupVisibility(false)}
                    xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="size-1">
                    <path strokeLinecap="round" strokeLinejoin="round" d="M6 18 18 6M6 6l12 12" />
                </svg>

            </div>

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