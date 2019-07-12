import React from 'react';
import api from '../apis/telecomOperator';
import CustomerList from './CustomerList';

class CustomerPhoneList extends React.Component {
    state = { phoneList : [] }

    onInputChange = async (customerId) => {
        const response = await api.get('/customers/' + customerId + '/phones');
        this.setState({ phoneList : response.data });
    }

    getIcon(activated) {
        if (activated)
            return <i className="icon checkmark" />

        return <i className="exclamation icon" />
    }

    renderPhoneList() {
        const phoneList = this.state.phoneList;

        if (!phoneList || phoneList.length === 0) {
            return (
                <tr>
                    <td colSpan="2">
                        No records found
                    </td>
                </tr>);
        }

        return phoneList.map ((item) => {
            return (
                <tr key={"phone-" + item.id }>
                    <td>{item.phoneNo}</td>
                    <td>{this.getIcon(item.activated)}{item.activated ? "Activated" : "Inactive"}</td>
                </tr>
            );
        });
    }

    render() {
        return (
            <div>
                <CustomerList onCustomerSelected={this.onInputChange} />
                <table className="ui very basic table">
                    <tbody>
                        {this.renderPhoneList()}
                    </tbody>
                </table>
            </div>
        );
    };

};

export default CustomerPhoneList;
