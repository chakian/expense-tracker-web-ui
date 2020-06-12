import React from 'react';

const PrivateLayout = ({children}) => 
    <div>
        <h2>PRIVATE</h2>
        {children}
    </div>;

export { PrivateLayout };