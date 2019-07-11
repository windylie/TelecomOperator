import React from 'react';
import api from '../apis/telecomOperator';

class PhoneList extends React.Component {
    onButtonClick = async () => {
        const response = await api.get('/phones');
        console.log(response);
    }

    render()
    {
        return (
            <div>
                <button onClick={this.onButtonClick}>Show All Phone Numbers</button>
            </div>
        );
    }
}

export default PhoneList;
