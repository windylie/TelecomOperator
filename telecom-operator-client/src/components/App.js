import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Header from './Header';
import PhoneList from './PhoneList';
import CustomerPhoneList from './CustomerPhoneList';
import CreatePhone from './CreatePhone';
import ActivatePhone from './ActivatePhone';

const App = () => {
    return (
        <div className="ui container">
            <BrowserRouter>
                <div>
                    <Header />
                    <Route path="/" exact component={PhoneList} />
                    <Route path="/customer/show" component={CustomerPhoneList} />
                    <Route path="/customer/create" component={CreatePhone} />
                    <Route path="/customer/activate" component={ActivatePhone} />
                </div>
            </BrowserRouter>
        </div>
    );
};

export default App;
