const SUCCESS = true;
const FAILED = false;
const VirtualCurrency = 'VirtualCurrency';
const HCCode = 'HC';
const SCCode = 'SC';

handlers.CreateAccount = () => {
    var HighScore = {
        Score: 0
    };

    var result = UpdateUserReadOnlyData({
        HighScore: JSON.stringify(HighScore)
    });

    // Error control in client.
    log.info(JSON.stringify(result));
};

handlers.UpdateScore = (args) => {
    var highScore = args.highScore;

    var HighScore = {
        Score: highScore
    };

    var result = UpdateUserReadOnlyData({
        HighScore: JSON.stringify(HighScore)
    });

    // Error control in client.
    log.info(JSON.stringify(result));
};

handlers.GetLoginData = () => {
    var readOnlyData = GetUserReadOnlyData().Data;
    var inventory = GetUserInventory();
    var currency = GetCurrency(inventory);

    var loginData = {
        readOnlyData: readOnlyData,
        inventory: inventory.Inventory,
        currency: currency
    };

    return { loginData: loginData };
};

handlers.GetCurrency = () => {
    var currency = {
        currency: GetCurrency(GetUserInventory())
    };

    return currency;
};

handlers.AddSCCurrency = (args) => {
    var amount = args.amount;

    try {
        var result = AddUserUserVirtualCurrency(amount, SCCode);
        log.info(result);

        return { isSuccess: SUCCESS, balance: result.Balance };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILED, error: error.apiErrorInfo.apiError.error };
    }
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
    var request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    var result = server.UpdateUserReadOnlyData(request);

    return result;
};

/**
 * Gets the ReadOnlyData from the user with PlayFabId = currentPlayerId.
 * 
 * @param {undefined} data 
 * @returns {Object} All the ReadOnlyData, access it with .Data
 */
function GetUserReadOnlyData(data) {
    var request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    return server.GetUserReadOnlyData(request);
};

/**
 * 
 * @returns {Object} The inventory of the user with currentPlayerId.
 */
function GetUserInventory() {
    var request = {
        PlayFabId: currentPlayerId
    }

    return server.GetUserInventory(request);
};

/**
 * Adds a certain amount of currency to virtualCurrency.
 * 
 * @param {number} amount - The amount to be added to virtualCurrency
 * @param {string} virtualCurrency - The code for the virtual currency to change.
 * @returns {Object} Access with .Balance to check the balance of the user.
 */
function AddUserUserVirtualCurrency(amount, virtualCurrency) {
    var request = {
        Amount: amount,
        PlayFabId: currentPlayerId,
        VirtualCurrency: virtualCurrency
    };

    return server.AddUserVirtualCurrency(request);
};

/////////////////////////////////////////////////////////////////////////
//
//                               UTILS
//
/////////////////////////////////////////////////////////////////////////

function GetCurrency(inventory) {
    var currency = {
        softCoins: inventory[VirtualCurrency][SCCode],
        hardCoins: inventory[VirtualCurrency][HCCode]
    };

    return currency;
};