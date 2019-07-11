import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
    return (
        <div className="ui secondary pointing menu">
            <Link to="/" className="item">Show All Phones</Link>
            <Link to="/customer/show" className="item">Show Customer's Phones</Link>
            <Link to="/customer/create" className="item">Add Phone</Link>
            <Link to="/customer/activate" className="item">Activate Phone</Link>
        </div>
    );
};

export default Header;
