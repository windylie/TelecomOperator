import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Header from './Header';
import PhoneList from './PhoneList';
import CustomerPhoneList from './CustomerPhoneList';
import CreatePhone from './CreatePhone';

const App = () => {
    return (
        <div className="ui container">
            <BrowserRouter>
                <div>
                    <Header />
                    <Route path="/" exact component={PhoneList} />
                    <Route path="/customer/show" component={CustomerPhoneList} />
                    <Route path="/customer/create" component={CreatePhone} />
                </div>
            </BrowserRouter>
        </div>
    );
};

export default App;
