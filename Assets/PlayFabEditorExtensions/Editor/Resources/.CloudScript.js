const SUCCESS = 200;
const FAILED = 400;
const VirtualCurrency = 'VirtualCurrency';
const HCCode = 'HC';
const SCCode = 'SC';

handlers.CreateAccount = () => {
    let HighScore = {
        Score: 0
    };

    let result = UpdateUserReadOnlyData({
        HighScore: JSON.stringify(HighScore)
    });

    // Error control in client.
    log.info(JSON.stringify(result));
};

handlers.UpdateScore = (args) => {
    let highScore = args.highScore;

    let HighScore = {
        Score: highScore
    };

    let result = UpdateUserReadOnlyData({
        HighScore: JSON.stringify(HighScore)
    });

    // Error control in client.
    log.info(JSON.stringify(result));
};

handlers.GetLoginData = () => {
    let readOnlyData = GetUserReadOnlyData().Data;
    let inventory = GetInventory();
    let currency = GetCurrency(inventory);

    let loginData = {
        ReadOnlyData: readOnlyData,
        Inventory: inventory.Inventory,
        Currency: currency
    };

    return { LoginData: loginData };
};

/////////////////////////////////////////////////////////////////////////
//
//                          SERVER CALLS
//
/////////////////////////////////////////////////////////////////////////

/**
 * 
 * @param {Object} data - The object with all the data we want to update.
 * @returns {Object}
 */
function UpdateUserReadOnlyData(data) {
    let request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    let result = server.UpdateUserReadOnlyData(request);

    return result;
};

/**
 * Gets the ReadOnlyData from the user with PlayFabId = currentPlayerId.
 * 
 * @param {undefined} data 
 * @returns {Object} All the ReadOnlyData, access it with .Data
 */
function GetUserReadOnlyData(data) {
    let request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    let result = server.GetUserReadOnlyData(request);

    return result;
};

/**
 * 
 * @returns {Object} The inventory of the user with currentPlayerId.
 */
function GetInventory() {
    let request = {
        PlayFabId: currentPlayerId
    }

    return server.GetUserInventory(request);
};

/////////////////////////////////////////////////////////////////////////
//
//                               UTILS
//
/////////////////////////////////////////////////////////////////////////

function GetCurrency(inventory) {
    let currency = {
        SoftCoins: inventory[VirtualCurrency][SCCode],
        HardCoins: inventory[VirtualCurrency][HCCode]
    };

    return currency;
};