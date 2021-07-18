const SUCCESS = true;
const FAILURE = false;
const VirtualCurrency = 'VirtualCurrency';
const HCCode = 'HC';
const SCCode = 'SC';

handlers.CreateAccount = function () {
    var HighScore = {
        Score: 0
    };

    try {
        var result = UpdateUserReadOnlyData({
            HighScore: JSON.stringify(HighScore)
        });
        log.info(result);

        return { isSuccess: SUCCESS, error: "" };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.UpdateScore = function (args) {
    var highScore = args.highScore;

    var HighScore = {
        Score: highScore
    };

    try {
        var result = UpdateUserReadOnlyData({
            HighScore: JSON.stringify(HighScore)
        });
        log.info(result);

        return { isSuccess: SUCCESS, error: ""  };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.GetLoginData = function () {
    try {
        var readOnlyData = GetUserReadOnlyData().Data;

        try {
            var catalog = GetCatalogItems().Catalog;

            try {
                var inventory = GetUserInventory();
                var currency = GetCurrency(inventory);

                var loginData = {
                    readOnlyData: readOnlyData,
                    catalog: catalog,
                    inventory: inventory.Inventory,
                    currency: currency
                };

                log.info(loginData);

                return { isSuccess: SUCCESS, loginData: loginData };

            } catch (error) {
                log.error("[ERROR GETTING THE USER INVENTORY] " + error);

                return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
            }
        } catch (error) {
            log.error("[ERROR GETTING THE CATALOG] " + error);

            return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
        }
    } catch (error) {
        log.error("[ERROR GETTING THE USER READ ONLY DATA] " + error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.GetCurrency = function () {
    try {
        var currency = GetCurrency(GetUserInventory())
        log.info(currency);

        return { isSuccess: SUCCESS, currency: currency };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

handlers.AddSCCurrency = function (args) {
    var amount = args.amount;

    try {
        var result = AddUserUserVirtualCurrency(amount, SCCode);
        log.info(result);

        return { isSuccess: SUCCESS, balance: result.Balance };
    } catch (error) {
        log.error(error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

/**
 * 
 * @param {object} args - The arguments from ExecuteCloudScriptRequest.
 * @param {string} args.itemInstanceId - The unique identifier of the puchased item.
 * @param {string} args.itemId - The item's id from the catalog.
 * @returns Wether the function was executed succesfully or not; if fails, in error there will be a vague description of the error.
 */
handlers.UpdateUserInventoryItemCustomData = function (args) {
    var itemInstanceId = args.itemInstanceId;
    var itemId = args.itemId;

    try {
        var catalog = GetCatalogItems().Catalog;
        var itemCustomData;

        for (let index = 0; index < catalog.length; index++) {
            if (catalog[index].ItemId == itemId) {
                itemCustomData = JSON.parse(catalog[index].CustomData);
                break;
            }
        }

        try {
            var result = UpdateUserInventoryItemCustomData(itemInstanceId, itemCustomData);
            log.info(result);

            return { isSuccess: SUCCESS };
        } catch (error) {
            log.error("[ERROR UPDATING THE USER INVENTORY] " + error);

            return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
        }

    } catch (error) {
        log.error(["ERROR GETTING THE CATALOG ITEMS"] + error);

        return { isSuccess: FAILURE, error: error.apiErrorInfo.apiError.error };
    }
};

/////////////////////////////////////////////////////////////////////////
//
//                          SERVER CALLS
//
/////////////////////////////////////////////////////////////////////////

/**
 * 
 * @param {object} data - The object with all the data we want to update.
 * @returns {object} .DataVersion has the version that has been set after the update.
 * @throws Will throw an error if the API encounters an error.
 */
function UpdateUserReadOnlyData(data) {
    var request = {
        PlayFabId: currentPlayerId,
        Data: data
    };

    return server.UpdateUserReadOnlyData(request);
};

/**
 * Gets the ReadOnlyData from the user with PlayFabId = currentPlayerId.
 * 
 * @param {undefined} data - Do not set any value in this parameter.
 * @returns {object} All the ReadOnlyData, access it with .Data
 * @throws Will throw an error if the API encounters an error.
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
 * @returns {object} The inventory of the user with currentPlayerId.
 * @throws Will throw an error if the API encounters an error.
 */
function GetUserInventory() {
    var request = {
        PlayFabId: currentPlayerId
    }

    return server.GetUserInventory(request);
};

/**
 * 
 * @param {string} [catalogVersion=null] - Which catalog is requested.
 * @returns {object[]} Access with .Catalog to obtain the List of items belonging to catalogVersion.
 * @throws Will throw an error if the API encounters an error.
 */
function GetCatalogItems(catalogVersion = null) {
    var request = {
        CatalogVersion: catalogVersion
    };

    return server.GetCatalogItems(request);
};

/**
 * Adds a certain amount of currency to virtualCurrency.
 * 
 * @param {number} amount - The amount to be added to virtualCurrency
 * @param {string} virtualCurrency - The code for the virtual currency to change.
 * @returns {object} Access with .Balance to check the balance of the user.
 * @throws Will throw an error if the API encounters an error.
 */
function AddUserUserVirtualCurrency(amount, virtualCurrency) {
    var request = {
        Amount: amount,
        PlayFabId: currentPlayerId,
        VirtualCurrency: virtualCurrency
    };

    return server.AddUserVirtualCurrency(request);
};

/**
 *  
 * @param {string} itemInstanceId - Unique identifier of the item to add to the user inventory.
 * @param {object} data - Dictionary to be written to the custom data.
 * @throws Will throw an error if the API encounters an error.
 */
function UpdateUserInventoryItemCustomData(itemInstanceId, data) {
    var request = {
        ItemInstanceId: itemInstanceId,
        PlayFabId: currentPlayerId,
        Data: data
    };

    server.UpdateUserInventoryItemCustomData(request);
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