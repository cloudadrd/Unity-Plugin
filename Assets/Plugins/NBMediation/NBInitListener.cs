using System;

public interface NBInitListener {
    void onSuccess();

    void onError(string message);
}
