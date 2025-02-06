import { Account } from "../../services/accountService";
import { useState } from "react";
import { ToastContainer, toast } from 'react-toastify';

function RegisterForm() {
    const [initialUserData, setInitialUserData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        dob: '2000-01-01',
        newsletterSubscribtion: true,
        address: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setInitialUserData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const registerUser = (formData) => {
        let firstName = formData.get('firstName').trim();
        let lastName = formData.get('lastName').trim();
        let email = formData.get('email').trim();
        let password = formData.get('password').trim();
        let confirmPassword = formData.get('confirmPassword').trim();
        let newsletterSubscribtion = formData.get('newsletterCheckbox');
        let address = formData.get('address').trim();

        //perfom validation
        //TODO -> extends validation to check if email is indeed email, name only characters etc; 

        if (lastName == '' || firstName == '' || email == '' || address == '') {
            toast.error("All fields are required for registration! Please, try again.");
            return;
        }

        if (password !== confirmPassword) {
            toast.error("Password and confirm password do not match!")
            return;
        }

        let checkboxValue = newsletterSubscribtion === 'on' ? true : false;

        initialUserData.firstName = firstName;
        initialUserData.lastName = lastName;
        initialUserData.email = email;
        initialUserData.address = address;
        initialUserData.newsletterSubscribtion = checkboxValue;
        initialUserData.password = password;

        Account.register(initialUserData).then(res => {
            if (res.succeeded) {
                toast.success("Registration successful.Taking you to login page in 3, 2, 1 .. ");

                setTimeout(() => {
                    window.location.href = "/";
                }
                    , 3000);
            }
        }).catch(err => {
            let errorMessage = '';
            err.response.data.forEach(error => {
                errorMessage += error.description;
                errorMessage += ' ';
            })
            toast.error(errorMessage);
        })
    }

    return (

        <div className="register">
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
                            value={initialUserData.firstName}
                            onChange={handleChange}
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
                            value={initialUserData.lastName}
                            onChange={handleChange}
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
                            value={initialUserData.email}
                            onChange={handleChange}
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="password" >Password</label>
                        <input
                            name="password"
                            type="password"
                            value={initialUserData.password}
                            onChange={handleChange}
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="confirmPassword" >Confirm password</label>
                        <input
                            name="confirmPassword"
                            type="password"
                            value={initialUserData.confirmPassword}
                            onChange={handleChange}
                        ></input>
                    </div>
                </div>

                <div className="register__row-address">
                    <label
                        htmlFor="address">Address</label>
                    <input
                        className="register__row-address"
                        name="address"
                        type="address"
                        value={initialUserData.address}
                        onChange={handleChange}
                    ></input>
                </div>

                <div className="register__row">
                    <label
                        htmlFor="dob">Date of birth</label>
                    <input
                        name="dob"
                        type="date"
                        min="1940-01-01"
                        max="2007-01-21"
                        value={initialUserData.dob}
                        onChange={handleChange}
                    ></input>
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