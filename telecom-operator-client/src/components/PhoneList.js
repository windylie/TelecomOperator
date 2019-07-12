import React from 'react';
import api from '../apis/telecomOperator';

class PhoneList extends React.Component {
    state = { phoneList : [] };

    renderTableBody()
    {
        const { phoneList } = this.state;

        if (!phoneList || phoneList.length === 0) {
            return (
                <tr>
                    <td colSpan="3" className="center aligned">
                        No records found
                    </td>
                </tr>);
        }

        return phoneList.map((item) => {
            return (
                <tr key={"phone-" + item.id }>
                  <td data-label="Name">{item.customerName}</td>
                  <td data-label="PhoneNo">{item.phoneNo}</td>
                  <td data-label="Activated">{item.activated ? "Activated" : "Inactive"}</td>
                </tr>
            );
        });
    }

    async componentDidMount() {
        const response = await api.get('/phones');
        this.setState({ phoneList : response.data });
    }

    render()
    {
        return (
            <div>
                <table className="ui celled table">
                    <thead>
                        <tr>
                            <th>Customer Name</th>
                            <th>Phone Number</th>
                            <th>Activated</th>
                        </tr>
                    </thead>
                    <tbody>
                        { this.renderTableBody() }
                    </tbody>
                </table>
            </div>
        );
    }
}

export default PhoneList;
