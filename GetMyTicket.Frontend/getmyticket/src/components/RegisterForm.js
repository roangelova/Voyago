import { Account } from "../services/accountService";

import { useActionState } from "react";

import { ToastContainer, toast } from 'react-toastify';


function RegisterForm() {

    let initialUserData = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        dob: '',
        isSubscribedForNewsletter: true,
        address: '',
    };

    const registerUser = (formData) => {
        isPending = true;

        let password = formData.get('password');
        let confirmPassword = formData.get('confirmPassword');

        //perfom validation
        if (password !== confirmPassword) {
            toast("Password and confirm password do not match!")
        }

        //IF VALIDATION FAILED - RETURN AND ADD TOAST

        //TODO: implement registration


        //if successful
        isPending = false;
    }

    let [state, formAction, isPending] = useActionState(initialUserData, registerUser, false);


    return (

        <div className="register">
            <ToastContainer />
            <form action={registerUser} className="register__container register__form">

                <div className="register__row">
                    <div>
                        <label htmlFor="firstName">First name</label>
                        <input
                            name="firstName"
                            type="text"
                            placeholder="First name"
                            autoComplete="on"
                            autoCapitalize="on"
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="lastName" >Last name</label>
                        <input
                            name="lastName"
                            type="text"
                            placeholder="Last Name"
                            autoComplete="on"
                            autoCapitalize="on"
                        ></input>
                    </div>
                </div>

                <div className="register__row">
                    <div>
                        <label htmlFor="email">Email</label>
                        <input
                            name="email"
                            type="email"
                            placeholder="email@gmail.com"
                            autoComplete="on"
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="password" >Password</label>
                        <input
                            name="password"
                            type="password"
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="confirmPassword" >Confirm password</label>
                        <input
                            name="confirmPassword"
                            type="password"
                        ></input>
                    </div>
                </div>

                <div className="register__row-address">
                    <label
                        htmlFor="address">Address</label>
                    <input
                        className="register__row-address"
                        name="address"
                        type="address"></input>
                </div>

                <div className="register__row-checkbox">
                    <label
                        htmlFor="newsletterCheckbox">Send me special offers & other updates by email.</label>
                    <input
                        name="newsletterCheckbox"
                        type="checkbox"
                        defaultChecked="true"></input>
                </div>

                <div className="register__btn">
                    <button
                    >
                        Register
                    </button>
                </div>
            </form>
        </div>

    )
}

export default RegisterForm;