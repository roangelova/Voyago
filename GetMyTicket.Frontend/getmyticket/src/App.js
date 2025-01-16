import { BrowserRouter, Route, Routes } from 'react-router';

import Header from './components/Header';
import RegisterForm from './components/RegisterForm';
import RegisterPopUp from './components/RegisterPopUp';

function App() {
  return (
    <div >

      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Header />} />
          <Route path="/register" element={<RegisterPopUp />} />
          <Route path="/register-form" element={<RegisterForm />} />
        </Routes>
      </BrowserRouter>

    </div >
  );
}

export default App;